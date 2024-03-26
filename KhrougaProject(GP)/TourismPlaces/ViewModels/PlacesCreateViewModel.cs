using System.ComponentModel.DataAnnotations;
using TourismPlaces.Data.Models;

namespace TourismPlaces.ViewModels
{
    public class PlacesCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public decimal EntrancePrice { get; set; }
      
        public DateTime DateOfCreation { get; set; }
        [Required]
        public decimal rate { get; set; }
        [Required]
        public string Details { get; set; }

        public int GovernmentId { get; set; }
        public string PhotoPath { get; set; }
        public bool IsDeleted { get; set; }
        public List<IFormFile> photos { get; set; }
    }
}
