using System.ComponentModel.DataAnnotations;

namespace TourismPlaces.ViewModels
{
    public class PlaceViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Place Name is Required")]
        [MinLength(5, ErrorMessage = "Place Name must be 5 characters to 50")]
        [MaxLength(50, ErrorMessage = "Place Name must be 5 characters to 50")]
        public string Name { get; set; }
        public String Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal EntrancePrice { get; set; }
        public List<String> PhoneNumbers { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }

        public string Details { get; set; }
        public int GovernmentId { get; set; }
        public int UserId { get; set; }
        public int PictureId { get; set; }
        public GovernmentViewModel Government { get; set; }

    }
}
