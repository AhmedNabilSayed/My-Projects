using Demo.DAL.Enteties;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
			this.signInManager = signInManager;
		}

        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email.Split('@')[0],
                    IsAgree = registerViewModel.IsAgree,
                };


                var result = await userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("LogIn");

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerViewModel);
        }
		#endregion

		#region SignIn
		public IActionResult LogIn(string? ReturnUrl)
		{
			return View(new SignInViewModel());
		}

        [HttpPost]
        public async Task<IActionResult> LogIn(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(signInViewModel.Email);

                if (user is null)
                    ModelState.AddModelError("", "Email Does Not Exist");
                var isCorrectPassword = await userManager.CheckPasswordAsync(user, signInViewModel.Password);

                if(isCorrectPassword)
                {
                    var result = await signInManager.PasswordSignInAsync(user, signInViewModel.Password, signInViewModel.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
            }

            return View(signInViewModel);
        }
        #endregion

        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(LogIn));
        }
        #endregion

        #region Reset Password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                    var email = new Email
                    {
                        Body = resetPasswordLink ,
                        Title = "Reset Password",
                        To = model.Email
                    };

                    EmailSettings.SendEmail(email);

                    return RedirectToAction("CompleteForgetPassword");
                }

                ModelState.AddModelError("", "Invalid Email"); 
            }
            return View();
        }


        public IActionResult CompleteForgetPassword()
        {
            return View();
        }


        #endregion


        public IActionResult ResetPassword(string Email , string Token)
        {
            return View(new ResetPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token ,model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("LogIn");
                    }

                    foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                }
            }
            return View(new ResetPasswordViewModel());
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
