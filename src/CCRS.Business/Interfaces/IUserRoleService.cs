using Microsoft.AspNetCore.Identity;

namespace CCRS.Business.Interfaces
{
    public interface IUserRoleService
    {
        Task AddRole(IdentityUser user);
    }
}

