namespace TourismPlaces.ViewModels.Admin
{
    public class RoleDetailsViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}
