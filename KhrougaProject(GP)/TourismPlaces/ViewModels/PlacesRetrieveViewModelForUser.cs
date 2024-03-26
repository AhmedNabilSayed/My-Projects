using System.ComponentModel.DataAnnotations;
using TourismPlaces.Data.Models;

namespace TourismPlaces.ViewModels
{
    public class PlacesRetrieveViewModelForUser
    {

        [Display(Name="Place Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal EntrancePrice { get; set; }
        public decimal Rate { get; set; }
        public string Details { get; set; }
        public string GovernmentName { get; set; }
        public string PhotoPath{ get; set; }//لوباظت نرجع هنا
        public string UserId { get; set; }
        //public bool IsDeleted { get; set; }
        //public bool IsApproved { get; set; }
        //public int PhotosNumber { get; set; }
        //public List<Photo> Photos{ get; set; }



    }
}