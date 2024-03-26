using System.ComponentModel.DataAnnotations;

namespace TourismPlaces.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5, ErrorMessage = "MinimumLength Is 5")]
        public string Password { get; set; }


        public bool RememberMe { get; set; }



    }
}
