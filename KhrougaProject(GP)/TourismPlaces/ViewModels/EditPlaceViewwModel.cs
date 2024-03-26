namespace TourismPlaces.ViewModels
{
    public class EditPlaceViewwModel
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public String Address { get; set; }
        public decimal EntrancePrice { get; set; }
        public DateTime DateOfCreation { get; set; }
        public decimal rate { get; set; }
        public string Details { get; set; }
        public int GovernmentId { get; set; }
        public List<PhotoSelectViewModel> Photos { get; set; }
        public bool IsDeleted { get; set; }
        public List<IFormFile>? PhotosToAdd { get; set; }
    }
}
