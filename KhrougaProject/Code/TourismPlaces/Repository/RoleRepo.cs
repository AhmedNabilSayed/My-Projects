using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourismPlaces.Data;
using TourismPlaces.Data.Models;
using TourismPlaces.IRepository;
using TourismPlaces.ViewModels.Admin;

namespace TourismPlaces.Repository
{
    public class RoleRepo : IRoleRepo
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleRepo(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._context = context;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        public async Task<bool> IsUserInRole(UserViewModel user, string role)
        {

            return await _userManager.IsInRoleAsync(_mapper.Map<ApplicationUser>(user), role);
        }

        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync()
        {
            var rolesInDb = await _context.Roles.ToListAsync();
            var roleViewList = new List<RoleViewModel>();
            foreach (var item in _context.Roles)
            {

                if (item.Name != "admin")
                {
                    RoleViewModel roleViewModel = new RoleViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    roleViewList.Add(roleViewModel);
                }
            }

            return roleViewList;
        }

        public async Task<RoleViewModel> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return null;
            }
            _context.Entry(role).State = EntityState.Detached;
            return _mapper.Map<RoleViewModel>(role);

        }

        public async Task<UserViewModel> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
           

            return _mapper.Map<UserViewModel>(user);

        }

        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            var usersInDb = await _userManager.Users.ToListAsync();
            var users = _mapper.Map<List<UserViewModel>>(usersInDb);
            return users;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(UserViewModel user, string role)
        {
            var applicationUser = await _userManager.FindByIdAsync(user.Id);
           
            var result = await _userManager.AddToRoleAsync(applicationUser, role); 
            return result;
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(UserViewModel user, string role)
        {
            var applicationUser =await _userManager.FindByIdAsync(user.Id);
            
            var result = await _userManager.RemoveFromRoleAsync(applicationUser, role);
            return result;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsersByRoleAsync(string roleName)
        {
            var appUsersList = await _userManager.GetUsersInRoleAsync(roleName);
            return _mapper.Map<List<UserViewModel>>(appUsersList);
        }
    }
}
