using System.ComponentModel.DataAnnotations;

namespace TourismPlaces.ViewModels
{
    public class PlaceRemoveViewModel
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }       
        public String Address { get; set; } 
        public decimal EntrancePrice { get; set; }
        public DateTime DateOfCreation { get; set; }
        public decimal rate { get; set; }
        public string Details { get; set; }
        public int GovernmentId { get; set; }
        public List<PhotoViewModel> Photos { get; set; }
       
        public string FirstImg{ get; set; }

       public PhotosFileViewModel? MorePhotos { get; set; } = new PhotosFileViewModel();



    }
}
