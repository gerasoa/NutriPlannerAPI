using CCRS.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CCRS.Business.Services
{
    public class DoctorRoleService : IUserRoleService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DoctorRoleService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddRole(IdentityUser user)
        {
            await _userManager.AddClaimAsync(user, new Claim("Role", "Doctor"));
        }
    }

    public class PatientRoleService : IUserRoleService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public PatientRoleService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddRole(IdentityUser user)
        {
            await _userManager.AddClaimAsync(user, new Claim("Role", "Patient"));
        }
    }

    public class UserRoleFactory
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleFactory(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IUserRoleService GetRoleService(string role)
        {
            return role switch
            {
                "Doctor" => new DoctorRoleService(_userManager),
                "Patient" => new PatientRoleService(_userManager),
                _ => throw new ArgumentException("Role não identificada.")
            };
        }
    }
}

