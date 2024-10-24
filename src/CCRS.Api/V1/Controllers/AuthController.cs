using Azure.Core;
using CCRS.Api.Controllers;
using CCRS.Api.Extensions;
using CCRS.Api.ViewModels;
using CCRS.Business.Interfaces;
using CCRS.Business.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CCRS.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly UserRoleFactory _roleFactory;

        public AuthController(INotifier notifier,
                SignInManager<IdentityUser> signUserManager,
                UserManager<IdentityUser> userManager,
                IOptions<AppSettings> appSettings,
                IUser user,
                IEmailService emailService,
                IEmailTemplateService emailTemplateService,
                UserRoleFactory roleFactory) : base(notifier, user)
        {
            _signInManager = signUserManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _roleFactory = roleFactory;
        }

        /// <summary>
        /// Registers a new user in the system, either as a doctor or a patient, based on the provided information.
        /// </summary>
        /// <param name="registerUser">Object containing the user's registration details, including their email and password.</param>
        /// <returns>
        /// Returns an HTTP 200 OK response with a JWT token if the registration is successful.
        /// Returns an HTTP 400 Bad Request response if the registration fails or the model is invalid.
        /// </returns>
        /// <remarks>
        /// This method creates a new user in the system and assigns them a role as either "Doctor" or "Patient". 
        /// The role is stored as a claim in the user's identity. After successful registration, the user is 
        /// automatically signed in, and a JWT token is generated and returned for authentication purposes.
        /// </remarks>
        /// <response code="200">User registered successfully. Returns a JWT token.</response>
        /// <response code="400">Invalid input data or registration failed due to business rules.</response>
        [HttpPost("new-account")]
        public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var existingUser = await _userManager.FindByIdAsync(registerUser.Email);
            if (existingUser != null)
            {
                NotifyError("Email already registered.");
                return CustomResponse();
            }

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                // Use a fábrica já injetada
                var roleService = _roleFactory.GetRoleService(registerUser.UserRole.ToString());
                await roleService.AddRole(user);

                await _signInManager.SignInAsync(user, false);

                // Envia o email de verificação
                await SendEmail(registerUser, user);


                //return CustomResponse(registerUser);
                return CustomResponse(await GerarJwt(user.Email));
            }
            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse(registerUser);
        }

        private async Task SendEmail(RegisterUserViewModel registerUser, IdentityUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token }, Request.Scheme);
            var htmlMessage = _emailTemplateService.GenerateEmailConfirmationTemplate(callbackUrl);

            await _emailService.SendHtmlEmailAsync(registerUser.Email, "Confirm your email", htmlMessage);
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token upon successful login.
        /// </summary>
        /// <param name="loginUser">The <see cref="LoginUserViewModel"/> containing the user's email and password for authentication.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> that includes a JWT token if login is successful.
        /// </returns>
        /// <response code="200">Returns if the login is successful, along with the generated JWT token.</response>
        /// <response code="400">Returns a bad request if the provided input data is invalid.</response>
        /// <response code="401">Returns an unauthorized status if the email or password is incorrect.</response>
        /// <response code="423">Returns if the user is temporarily locked out after multiple failed login attempts.</response>
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                //return CustomResponse(loginUser);
                //_logger.LogInformation("Usuario " + loginUser.Email + " logado com sucesso");
                //return CustomResponse(await GerarJwt(loginUser.Email));
                return CustomResponse(await GerarJwt(loginUser.Email));
            }
            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotifyError("Usuário ou Senha incorretos");
            return CustomResponse(loginUser);
        }


        /// <summary>
        /// Confirms the email of a user based on the provided user ID and token.
        /// </summary>
        /// <param name="userId">The ID of the user whose email is to be confirmed.</param>
        /// <param name="token">The token used to confirm the user's email.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> that indicates the result of the email confirmation process.
        /// </returns>
        /// <response code="200">Returns if the email was successfully confirmed.</response>
        /// <response code="400">Returns if the request is invalid or if the email confirmation failed.</response>
        /// <response code="404">Returns if the user with the provided ID was not found.</response>
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid email confirmation request.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Confirm the email using the token
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                // Optionally sign in the user or redirect to a confirmation page
                return Ok("Email confirmed successfully.");
            }

            // If we reach here, something went wrong
            return BadRequest("Error confirming email.");
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        
    }
}
