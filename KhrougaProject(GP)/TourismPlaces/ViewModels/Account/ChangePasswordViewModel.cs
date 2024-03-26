using System.ComponentModel.DataAnnotations;

namespace TourismPlaces.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="New Password is required")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = " Confirm Password  is required.")]
        [Compare("NewPassword", ErrorMessage = "New Password and confirm password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
