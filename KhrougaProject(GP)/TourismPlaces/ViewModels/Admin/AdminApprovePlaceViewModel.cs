namespace TourismPlaces.ViewModels.Admin
{
    public class AdminApprovePlaceViewModel
    {
        public string PlaceName{ get; set; }
        public decimal Rate { get; set; }
        public decimal EntryPrice { get; set; }
        public string CovernmentName { get; set; }
        public string Owner { get; set; }
        public int PlaceId { get; set; }
        public bool IsApproved { get; set; }

    }
}
