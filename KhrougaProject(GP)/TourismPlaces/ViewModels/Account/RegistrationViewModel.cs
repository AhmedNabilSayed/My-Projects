using System.ComponentModel.DataAnnotations;
using TourismPlaces.Data.Models;

namespace TourismPlaces.ViewModels.Account
{
    public class RegistrationViewModel
    {
       
        [Required(ErrorMessage ="user name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email (ex user@domain.com)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is reguired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm your password")]
        [Compare("Password", ErrorMessage = "Password and confirm password don't match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Please choose a government")]
        public int GovernmentId { get; set; }



    }
}
