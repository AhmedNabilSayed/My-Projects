using Demo.DAL.Enteties;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.PL.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ILogger logger;
		private readonly UserManager<ApplicationUser> userManager;

		public RolesController(RoleManager<ApplicationRole> roleManager , ILogger<RolesController> logger,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.logger = logger;
			this.userManager = userManager;
		}
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return NotFound();

            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();
            return View(viewName, role);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole appRole)
        {
            if (id != appRole.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await roleManager.FindByIdAsync(id);

                    role.Name = appRole.Name;
                    role.NormalizedName = appRole.Name.ToUpper();

                    var result = await roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
            return View(appRole);

        }

        public async Task<IActionResult> Delete(string id, ApplicationRole appRole)
        {
            if (id != appRole.Id)
                return NotFound();

            try
            {
                var role = await roleManager.FindByIdAsync(id);


                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);


            if(role == null)
                return BadRequest();

            ViewBag.RoleId = roleId;

            var usersInRole = new List<UserInRoleViewModel>();

            foreach(var user in await userManager.Users.ToListAsync())
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if(await userManager.IsInRoleAsync(user , role.Name))
                   userInRole.IsSelected = true;
                else
                   userInRole.IsSelected = false;
                
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleViewModel> users ,string roleId)
        {
           
            var role = await roleManager.FindByIdAsync(roleId);

            if(role == null)
                return BadRequest();

            if(ModelState.IsValid)
            {
                foreach(var user in users)
                {
                    var appUser = await userManager.FindByIdAsync(user.UserId);

                    if(appUser != null)
                    {
                        if (user.IsSelected && !(await userManager.IsInRoleAsync(appUser, role.Name)))
                            await userManager.AddToRoleAsync(appUser,role.Name);
                        else if (!user.IsSelected && (await userManager.IsInRoleAsync(appUser, role.Name)))
                            await userManager.RemoveFromRoleAsync(appUser, role.Name);

                    }


                }
                return RedirectToAction(nameof(Update) , new { id = roleId });
            }
            return View(users);
        }
    }
}
