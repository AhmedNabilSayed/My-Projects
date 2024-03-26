
using Microsoft.AspNetCore.Identity;
using TourismPlaces.Data.Models;
using TourismPlaces.ViewModels.Admin;

namespace TourismPlaces.IRepository
{
    public interface IRoleRepo
    {
        Task<IEnumerable< RoleViewModel>> GetRolesAsync();

        Task<RoleViewModel> GetRoleByIdAsync(string id);
        Task<UserViewModel> GetUserByIdAsync(string id);

        Task<bool> IsUserInRole(UserViewModel user,string role);

        Task<IEnumerable< UserViewModel>> GetUsersAsync();

        Task<IdentityResult> AddUserToRoleAsync(UserViewModel user, string role);

        Task<IdentityResult> RemoveFromRoleAsync(UserViewModel user, string role);


        Task<IEnumerable<UserViewModel>> GetUsersByRoleAsync(string roleName);



    }
}
