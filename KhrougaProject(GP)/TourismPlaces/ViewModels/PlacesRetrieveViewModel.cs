using System.ComponentModel.DataAnnotations;

namespace TourismPlaces.ViewModels
{
    public class PlacesRetrieveViewModel
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
      
        public String Address { get; set; }
       
        public decimal EntrancePrice { get; set; }

        public DateTime DateOfCreation { get; set; }
       
        public decimal rate { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsApproved { get; set; }
        public string GovernmentName { get; set; }
        public int PhotosNumber{ get; set; }

    }
}
