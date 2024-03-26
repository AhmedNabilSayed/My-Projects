using TourismPlaces.ViewModels.Dashboard;

namespace TourismPlaces.IRepository
{
	public interface IDashboardBoardRepo
	{
		Task<GetAllUsersNumberViewModel> GetCountAsync();

		List<GetGovenmentPlacesCountViewModel> GetGovenmentPlacesCount();
        Dictionary<string, Dictionary<string, UsersInGovernment>> GetUserNumberInGovernment();

		Task<IEnumerable<AdminInformationViewModel>> GetAdminInformation();


		Task<PlacesAndUsersCountViewModel> GetPlacesAndUsersCOuntAsync();

		Task<PlacesApprovedCountViewModel> GetPlacesApprovedCOuntAsync();

		Task<List<GetGovenmentPlacesCountViewModel>> GetGovenmentPlacesCountByOwnerId();
    }
}
