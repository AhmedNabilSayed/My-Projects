using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TourismPlaces.Data;
using TourismPlaces.Data.Models;
using TourismPlaces.IRepository;
using TourismPlaces.ViewModels.Dashboard;

namespace TourismPlaces.Repository
{
    public class DashboardBoardRepo : IDashboardBoardRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DashboardBoardRepo(ApplicationDbContext context, UserManager<ApplicationUser> userManager ,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }



        public async Task<GetAllUsersNumberViewModel> GetCountAsync()
        {
            int userCount = 0;
            int ownerCount = 0;
            var allUsers = _context.Users.ToList();
            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, "user"))
                {
                    userCount++;
                }
                else if (await _userManager.IsInRoleAsync(user, "owner"))
                {
                    ownerCount++;
                }
            }

            return new GetAllUsersNumberViewModel()
            {
                UserNumber = userCount,
                OwnerNumber = ownerCount,
                TotalCount = userCount + ownerCount
            };
        }

        public List<GetGovenmentPlacesCountViewModel> GetGovenmentPlacesCount()
        {
            var result = _context.Places
                .GroupBy(x => x.Government.Name)
                .Select(g =>
                new
                {
                    govermentName = g.Key,
                    PlacesNumbers = g.Count()
                });

            var governmentCountList = new List<GetGovenmentPlacesCountViewModel>();
            foreach (var govCount in result)
            {
                var model = new GetGovenmentPlacesCountViewModel()
                {
                    GovernmentName = govCount.govermentName,
                    PlacesCount = govCount.PlacesNumbers
                };
                governmentCountList.Add(model);
            }
            return governmentCountList;

        }

        public Dictionary<string, Dictionary<string, UsersInGovernment>> GetUserNumberInGovernment()
        {
            var result = _context.Users
                .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { User = u, UserRole = ur })
                .Join(_context.Roles, ur => ur.UserRole.RoleId, r => r.Id, (ur, r) => new { ur.User, r.Id, RoleName = r.Name })
                .Join(_context.Governments, u => u.User.GovernmentId, g => g.Id, (u, g) => new { g.Name, u.Id, u.RoleName })
                .GroupBy(x => new { x.Name, x.Id, x.RoleName })
                .Select(g => new { g.Key.Name, g.Key.Id, g.Key.RoleName, UserCount = g.Count() })
                .ToList()
                .GroupBy(x => x.Name)
                .ToDictionary(g => g.Key, g => g.ToDictionary(x => x.Id, x => new UsersInGovernment { RoleName = x.RoleName, UserCount = x.UserCount }));

            return result;
        }

        public async Task<IEnumerable<AdminInformationViewModel>> GetAdminInformation()
        {
            List<AdminInformationViewModel> adminInfoList = new List<AdminInformationViewModel>();

            var users =  _context.Users.ToList();
            foreach (var user in users)
            {
                try
                {
                    if (await _userManager.IsInRoleAsync(user, "owner"))
                    {
                        adminInfoList.Add(new AdminInformationViewModel
                        { UserName = user.UserName, Email = user.Email, RoleName = "owner" });
                    }
                    else if (await _userManager.IsInRoleAsync(user, "user"))
                    {
                        adminInfoList.Add(new AdminInformationViewModel
                        { UserName = user.UserName, Email = user.Email, RoleName = "user" });
                    }
                }
                catch (Exception ex)
                {

                    await Console.Out.WriteLineAsync(  ex.Message );
                }
            

            }
            return adminInfoList;
        }

        public async Task<PlacesAndUsersCountViewModel> GetPlacesAndUsersCOuntAsync()
        {
            int userCount = 0;
            int placesCount = 0;
            var allUsers = _context.Users.ToList();
            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, "user"))
                {
                    userCount++;
                }
                
            }
            var owner = await _userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
          placesCount =  _context.Places.Where(x => x.UserId == owner.Id).Count();

            return new PlacesAndUsersCountViewModel()
            {
                UserNumber = userCount,
               PlacesNumber = placesCount,
                TotalCount = userCount + placesCount
            };
        }

        public async Task<PlacesApprovedCountViewModel> GetPlacesApprovedCOuntAsync()
        {
            int pendingCount = 0;
            int approvedCount = 0;
            var owner = await _userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            var allPlaces = _context.Places.Where(c => c.UserId == owner.Id);

            approvedCount = allPlaces.Where(c => c.IsApproved == true).Count();
            pendingCount = allPlaces.Where(c => c.IsApproved == false).Count();


            return new PlacesApprovedCountViewModel()
            {
                ApprovedCount = approvedCount,
                PendingCount = pendingCount,
                TotalCount = approvedCount + pendingCount
            };
        }

        public async Task<List<GetGovenmentPlacesCountViewModel>> GetGovenmentPlacesCountByOwnerId()
        {
            var owner = await _userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            var result = _context.Places.Where(c=>c.UserId==owner.Id)
                .GroupBy(x => x.Government.Name)
                .Select(g =>
                new
                {
                    govermentName = g.Key,
                    PlacesNumbers = g.Count()
                });

            var governmentCountList = new List<GetGovenmentPlacesCountViewModel>();
            foreach (var govCount in result)
            {
                var model = new GetGovenmentPlacesCountViewModel()
                {
                    GovernmentName = govCount.govermentName,
                    PlacesCount = govCount.PlacesNumbers
                };
                governmentCountList.Add(model);
            }
            return governmentCountList;
        }
    }

}
