using CCRS.Business.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CCRS.Api.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }

        //public bool IsDoctor { get; set; }  // Indica se é um médico

        public UserRole UserRole { get; set; }

        //public Guid? DoctorId { get; set; }  // Se for paciente, este campo deve ter o ID do médico
    }

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }

    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
