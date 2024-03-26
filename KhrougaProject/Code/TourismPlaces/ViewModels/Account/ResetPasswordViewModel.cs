using System.ComponentModel.DataAnnotations;

namespace TourismPlaces.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = " Password  is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = " Confirm Password  is required.")]
        [Compare("Password",ErrorMessage = "Password and confirm password don't match")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


    }
}
