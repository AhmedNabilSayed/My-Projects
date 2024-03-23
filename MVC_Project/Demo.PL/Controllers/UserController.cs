using Demo.DAL.Enteties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<UserController> logger;

        public UserController(UserManager<ApplicationUser> userManager , ILogger<UserController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }
        public async Task<ActionResult> Index(string SearchValue = "")
        {
            IEnumerable<ApplicationUser> users;
            if(string.IsNullOrWhiteSpace(SearchValue))
                users = await userManager.Users.ToListAsync();
            else
                users = await userManager.Users.Where(user => user.Email.Trim().ToLower().Contains(SearchValue.Trim().ToLower())).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id , string viewName = "Details")
        {
            if(id is null)
                return NotFound();

            var user = await userManager.FindByIdAsync(id);

            if(user == null)
                return NotFound();
            return View(viewName, user);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id , ApplicationUser appUser)
        {
            if(id != appUser.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);

                    user.UserName = appUser.UserName;
                    user.NormalizedUserName = appUser.UserName.ToUpper();

                    var result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                }catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                } 
            }
            return View(appUser);

        }

        public async Task<IActionResult> Delete(string id, ApplicationUser appUser)
        {
            if (id != appUser.Id)
                return NotFound();

                try
                {
                    var user = await userManager.FindByIdAsync(id);


                    var result = await userManager.DeleteAsync(user);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }

            return RedirectToAction(nameof(Index));

        }
    }
}
