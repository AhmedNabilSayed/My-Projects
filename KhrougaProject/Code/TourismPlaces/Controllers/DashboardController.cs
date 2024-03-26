using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TourismPlaces.IRepository;
using TourismPlaces.ViewModels.Dashboard;

namespace TourismPlaces.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class DashboardController : Controller
    {
		private readonly IDashboardBoardRepo _dashboardBoardRepo;

		public DashboardController(IDashboardBoardRepo dashboardBoardRepo)
        {
			_dashboardBoardRepo = dashboardBoardRepo;
		}
        public async Task<IActionResult> Index()
        {
            /* Start of User vs owner chart */
            var result = await _dashboardBoardRepo.GetCountAsync();          
            List<DataPoint> dataPoints = new List<DataPoint>();
            dataPoints.Add(new DataPoint(result.UserNumber, "User Count", "#E7823A"));
            dataPoints.Add(new DataPoint(result.OwnerNumber, "Owner Count", "#546BC1"));
            ViewBag.NewVsReturningVisitors = JsonConvert.SerializeObject(dataPoints);
            ViewBag.TotalCount = result.TotalCount;
            
            //******************************************************************************************

            /* Start of GovernmentCount chart */
            var GovernmentPlacesCount = _dashboardBoardRepo.GetGovenmentPlacesCount();
            List<DataPointChart> dataPointsForChart = new List<DataPointChart>();
            foreach (var item in GovernmentPlacesCount)
            {
                dataPointsForChart.Add(new DataPointChart(item.GovernmentName, item.PlacesCount));
            }
            ViewBag.dataPointsGovernmentCount = JsonConvert.SerializeObject(dataPointsForChart);

            //******************************************************************************************


           var test= _dashboardBoardRepo.GetUserNumberInGovernment();
            List<DataPointChart> userDataPoints = new List<DataPointChart>();
            List<DataPointChart> ownerDataPoints = new List<DataPointChart>();

            foreach (var government in test)
            {
                var governmentData = government.Value;
                var userData = new DataPointChart(government.Key, 0);
              
                var ownerData = new DataPointChart(government.Key, 0);
                foreach (var role in governmentData)
                {
                    var roleData = role.Value;
                    if (roleData.RoleName == "user")
                    {
                        userData.Y += roleData.UserCount;
                    }
                    else if (roleData.RoleName == "owner")
                    {
                        ownerData.Y += roleData.UserCount;
                    }
                }
                userDataPoints.Add(userData);
                ownerDataPoints.Add(ownerData);
            }



            ViewBag.userDataPointsChart = JsonConvert.SerializeObject(userDataPoints);
            ViewBag.ownerDataPointsChart = JsonConvert.SerializeObject(ownerDataPoints);



            var es = await _dashboardBoardRepo.GetAdminInformation();
            ViewBag.AdminInfo = es;

            return View();
        }

        public async  Task<IActionResult> ChartsForOwner()
        {


            /* Start of User vs owner chart */
            var result = await _dashboardBoardRepo.GetPlacesAndUsersCOuntAsync();
            List<DataPoint> dataPoints = new List<DataPoint>();
            dataPoints.Add(new DataPoint(result.PlacesNumber, "Places Count", "#A4A832"));
            dataPoints.Add(new DataPoint(result.UserNumber, "Users Count", "#A83275"));
            ViewBag.NewVsReturningVisitors = JsonConvert.SerializeObject(dataPoints);
            ViewBag.TotalCount = result.TotalCount;

            //******************************************************************************************

            /* Start of User vs owner chart */
            var resultPlaceApproved = await _dashboardBoardRepo.GetPlacesApprovedCOuntAsync();
            List<DataPoint> dataPointsPlaceApproved = new List<DataPoint>();
            dataPointsPlaceApproved.Add(new DataPoint(resultPlaceApproved.ApprovedCount, "Approved Post", "#0F964A"));
            dataPointsPlaceApproved.Add(new DataPoint(resultPlaceApproved.PendingCount, "Pending Posts", "#E39D12"));
            ViewBag.resultPlaceApproved = JsonConvert.SerializeObject(dataPointsPlaceApproved);
            ViewBag.TotalCountPlaceApproved = resultPlaceApproved.TotalCount;

            //******************************************************************************************



            /* Start of GovernmentCount chart */
            var GovernmentPlacesCount =await _dashboardBoardRepo.GetGovenmentPlacesCountByOwnerId();
            List<DataPointChart> dataPointsForChart = new List<DataPointChart>();
            foreach (var item in GovernmentPlacesCount)
            {
                dataPointsForChart.Add(new DataPointChart(item.GovernmentName, item.PlacesCount));
            }
            ViewBag.dataPointsGovernmentCountByOwnerId = JsonConvert.SerializeObject(dataPointsForChart);

            //******************************************************************************************

            return View();
        }
    }
}
