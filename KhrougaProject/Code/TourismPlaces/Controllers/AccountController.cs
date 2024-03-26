using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using TourismPlaces.Data;
using TourismPlaces.Data.Models;
using TourismPlaces.Helper;
using TourismPlaces.ViewModels.Account;

namespace TourismPlaces.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IToastNotification _toast;

        public AccountController(ApplicationDbContext context ,UserManager<ApplicationUser> userManager
            ,RoleManager<IdentityRole> roleManager,IMapper mapper,SignInManager<ApplicationUser> signInManager,
            IToastNotification toast)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _toast = toast;
        }



        #region Registration

        public IActionResult Registration()
        {
            var data = _context.Governments.ToList();
            ViewBag.GovernmentList = new SelectList(data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel)
        {
            var data = _context.Governments.ToList();
            ViewBag.GovernmentList = new SelectList(data, "Id", "Name");

            if (ModelState.IsValid)
            {
                if (!registrationViewModel.Password.Equals(registrationViewModel.ConfirmPassword))
                {
                    _toast.AddErrorToastMessage("Something Went Wrong");
                    ModelState.AddModelError("Custom Errors", "Invaild Data");
                    return View(registrationViewModel);
                }
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = registrationViewModel.UserName,
                    Email = registrationViewModel.Email,
                    GovernmentId = registrationViewModel.GovernmentId,
                };
                var result = await _userManager.CreateAsync(user, registrationViewModel.Password);
                var role = _roleManager.FindByNameAsync("user").Result.ToString();

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    _toast.AddSuccessToastMessage("User Created Successfully ! Now you can login");
                    return RedirectToAction("Index", "Account");
                }
                foreach (var err in result.Errors)
                {
                    _toast.AddErrorToastMessage("Something Went Wrong");
                    ModelState.AddModelError("Custom Errors", err.Description);
                }




            }
            _toast.AddErrorToastMessage("Something Went Wrong");
            //ModelState.AddModelError("Custom Errors", "Invaild Data");
            return View(registrationViewModel);
        }
        #endregion


        #region Sign In

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("user"))
            {
                _toast.AddInfoToastMessage("Welcome To your home Page");
                return RedirectToAction("Index", "User");
            }
            if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
            {
                _toast.AddInfoToastMessage("Welcome To your home Page");
                return RedirectToAction("Index", "Admin");
            }
            if (User.Identity.IsAuthenticated && User.IsInRole("owner"))
            {
                _toast.AddInfoToastMessage("Welcome To your home Page");
                return RedirectToAction("Index", "Owner");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel,string? returnUrl)
        {
            if (ModelState.IsValid)
            {
             
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    var password = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (password)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password,
                            loginViewModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                         
                            if (await _userManager.IsInRoleAsync(user,"admin"))
                            {
                                _toast.AddInfoToastMessage("Welcome To your home Page");
                                return returnUrl is null ? RedirectToAction("Index", "Admin") : Redirect(returnUrl);
                              
                            }
                           if (await _userManager.IsInRoleAsync(user, "owner"))
                            {
                                _toast.AddInfoToastMessage("Welcome To your home Page");
                                return returnUrl is null ? RedirectToAction("Index", "Owner") : Redirect(returnUrl);
                               // return RedirectToAction("Index", "Owner");
                            }
                            if (await _userManager.IsInRoleAsync(user, "user"))
                            {
                                _toast.AddInfoToastMessage("Welcome To your home Page");
                                return returnUrl is null ? RedirectToAction("Index", "User") : Redirect(returnUrl);
                            
                            }

                        }

                        else
                        {

                            _toast.AddErrorToastMessage("Invalid Login attempt");
                            return View(loginViewModel);
                        }
                     

                    }

                }

                _toast.AddErrorToastMessage("Invalid Login attempt");

            }
            _toast.AddErrorToastMessage("Invalid Login attempt");
            return View(loginViewModel);
        }

        #endregion


        #region Sign Out

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("HomePage", "Account");
        }
        #endregion


        #region Forget Password

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink = Url.Action("ResetPassword", "Account"
                        , new { Email = forgetPasswordViewModel.Email, Token = token }, Request.Scheme);
                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = resetPasswordLink,
                        To = forgetPasswordViewModel.Email

                    };
                    //Method To Send Email
                    //go to your Google Account => Security => 2-Step Verification =>enable it
                    //App Passwords
                    EmailSettings.SendEmail(email);
                    _toast.AddSuccessToastMessage("Please check your email to reset password");
                    return RedirectToAction("Index","Account");

                }
                ModelState.AddModelError("Custom Error", "Invalid Email");


            }
            _toast.AddErrorToastMessage("Something Went Wrong");
            return View(forgetPasswordViewModel);
        }
        #endregion

        #region Reset Password

        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token,
                        resetPasswordViewModel.Password);
                    if (result.Succeeded)
                    {
                        _toast.AddSuccessToastMessage("Password reseted ssucccessfuly ! now you can login");
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(String.Empty, error.Description);
                    }
                   
                }

                           }

            return View(resetPasswordViewModel);
        }
        #endregion



        #region Change Password
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _toast.AddErrorToastMessage("Please login first to change password");
                    return RedirectToAction("Index", "Account");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        if (await _userManager.IsInRoleAsync(user, "admin"))
                        {
                            ModelState.AddModelError("Custom Error", err.Description);
                            _toast.AddErrorToastMessage("Invalid Password Please try again later ");
                            return RedirectToAction("ChangePassword", "Account");

                        }
                        if (await _userManager.IsInRoleAsync(user, "owner"))
                        {
                            ModelState.AddModelError("Custom Error", err.Description);
                            _toast.AddErrorToastMessage("Invalid Password Please try again later ");
                            return RedirectToAction("ChangePassword", "Account");

                        }
                        if (await _userManager.IsInRoleAsync(user, "user"))
                        {
                            ModelState.AddModelError("Custom Error", err.Description);
                            _toast.AddErrorToastMessage("Invalid Password Please try again later ");
                            return View(model);
                        }
                    }
                }
                 await _signInManager.RefreshSignInAsync(user);
                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    _toast.AddInfoToastMessage("Password has changed successfully");
                    // return  RedirectToAction("Index", "Admin") ;
                    return View();

                }
                if (await _userManager.IsInRoleAsync(user, "owner"))
                {
                    _toast.AddInfoToastMessage("Password has changed successfully");
                    // return RedirectToAction("Index", "Owner") ;
                    return View();
                }
                if (await _userManager.IsInRoleAsync(user, "user"))
                {
                    _toast.AddInfoToastMessage("Password has changed successfully");
                  //  return  RedirectToAction("Index", "User");
                    return View();
                }
                _toast.AddErrorToastMessage("Please login first to change password");
                return RedirectToAction("Index", "Account");
            }

            
            _toast.AddErrorToastMessage("Invalid Password Please try again later ");
            return View(model);
        }


        #endregion

        public IActionResult HomePage()
        {
            return View();
        }
    }
}
