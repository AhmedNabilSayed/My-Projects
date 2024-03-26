using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using TourismPlaces.IRepository;
using TourismPlaces.Repository;
using TourismPlaces.ViewModels;
using TourismPlaces.ViewModels.Admin;

namespace TourismPlaces.Controllers
{
    [Authorize(Roles ="admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AdminController : Controller
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IPlaceApproveRepo _placeApproveRepo;
        private readonly IToastNotification _toast;
        private readonly IPlaceRepo _placeRepo;

        public AdminController(IRoleRepo roleRepo , IPlaceApproveRepo placeApproveRepo, IToastNotification toast,IPlaceRepo placeRepo)
        {
            _roleRepo = roleRepo;
            _placeApproveRepo = placeApproveRepo;
            _toast = toast;
            _placeRepo = placeRepo;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index","Dashboard");
        }

        public async Task<IActionResult> Role()
        {
            return View(await _roleRepo.GetRolesAsync());
        }




        [HttpGet]
        public async Task<IActionResult> AddUsersToRole(string roleId)
            
        {
            ViewBag.roleId = roleId;
           var role= await _roleRepo.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                _toast.AddErrorToastMessage("This role can not be found");
                return View("Role","Admin");
            }
            var users = await _roleRepo.GetUsersAsync();
            var model = new List<UserRoleViewModel>();
            foreach (var user in users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email=user.Email,
                    

                };
                if (await _roleRepo.IsUserInRole(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> AddUsersToRole(List<UserRoleViewModel> model, string roleId)

        {
           
            var role = await _roleRepo.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                _toast.AddErrorToastMessage("This role can not be found");
                return View("Role", "Admin");
            }
            for (int i = 0; i < model.Count; i++)
            {
                IdentityResult result = null;

                var userViewModel = new UserViewModel()
                {
                    Id = model[i].UserId,
                    Email = model[i].Email,
                    UserName = model[i].UserName,
                };
                var userInRole = await _roleRepo.IsUserInRole(userViewModel, role.Name);

                if (model[i].IsSelected &&!userInRole)
                {
                    result = await _roleRepo.AddUserToRoleAsync(userViewModel, role.Name);
                }
                else if (!model[i].IsSelected && userInRole)
                {
                   result = await _roleRepo.RemoveFromRoleAsync(userViewModel, role.Name);
                }
                else
                   continue;
                
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    _toast.AddSuccessToastMessage("Roles of users has updated suucessfully");
                    return RedirectToAction("Role", "Admin");

                }
            }

            return RedirectToAction("Role", "Admin");
        }



        [HttpGet]
        public async Task<IActionResult> RoleDetails(string roleId)
        {
            var roleVm = await _roleRepo.GetRoleByIdAsync(roleId);
            if (roleVm ==null)
            {
                TempData["error"] = "something went bad";
                return View("Error");
            }
          var users = await  _roleRepo.GetUsersByRoleAsync(roleVm.Name);
            var roleDetailsViewModel = new RoleDetailsViewModel()
            {
                RoleId = roleId,    
                RoleName = roleVm.Name,
                Users = users.ToList()
            };

            return View(roleDetailsViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> ApprovePost()
        {
            return View(_placeApproveRepo.GetApprovedPlaces());
        }

      
          
        public async Task<IActionResult> ChangeApprove(int placeId)
        {
         var result= await _placeApproveRepo.ApprovePostAsync(placeId);
            if (result)
            {
                _toast.AddSuccessToastMessage("Post Have been Approved");
                return RedirectToAction("ApprovePost","Admin");
            }
            _toast.AddSuccessToastMessage("Something went wrong");
            return RedirectToAction("ApprovePost", "Admin");

        }




        [HttpGet]
        public async Task<IActionResult> PlaceDetails(int placeId)
        {
            var place = await _placeRepo.GetPlaceByIdAsync(placeId);
            var photos = _placeRepo.GetPhotoByPlaceId(placeId);

            var data = await _placeRepo.GetGovernmetsAsync();
            ViewBag.GovernmentList = new SelectList(data, "Id", "Name", place.GovernmentId);

            var model = new PlaceRemoveViewModel()
            {
                PlaceId = placeId,
                Name = place.Name,
                Address = place.Address,
                DateOfCreation = place.DateOfCreation,
                Details = place.Details,
                EntrancePrice = place.EntrancePrice,
                rate = place.rate,
                Photos = photos.ToList(),
                FirstImg = photos.ToList().FirstOrDefault().PhotoPath,
                GovernmentId = place.GovernmentId,
            };
            return View(model);
        }


      


    }


  
    

}
