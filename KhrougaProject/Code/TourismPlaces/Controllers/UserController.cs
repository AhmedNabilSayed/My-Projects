using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System.Data;
using System.Security.Claims;
using TourismPlaces.Data;
using TourismPlaces.IRepository;
using TourismPlaces.ViewModels;

namespace TourismPlaces.Controllers
{
    [Authorize(Roles = "user")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class UserController : Controller
    {
        private readonly IPlaceRepo _placeRepo;
        private readonly IToastNotification _toast;

        public UserController(IPlaceRepo placeRepo,IToastNotification toast)
        {
            _placeRepo = placeRepo;
            _toast = toast;
        }
        #region Index

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var places = await _placeRepo.GetAllPlacesForUser();
            places.Where(p => p.UserId == userId)
                .ToList();

            return View(places);
        }
        #endregion

        #region PlacesInGovernment

        [HttpGet]
        public async Task<IActionResult> PlacesInGovernment(int Id)
        {
            var government = await _placeRepo.GetGovernmentByIdAsync(Id);
            var places = await _placeRepo.GetAllPlacesInGov(Id);
            //ViewBag.GovernmentName = government.Name;
            return View(places);
        }
        #endregion

        #region  PLACE DETAILS

        [HttpGet]
        public async Task<IActionResult> PlaceDetails(int placeId)
        {
            var place = await _placeRepo.GetPlaceByIdAsync(placeId);
            var photos = _placeRepo.GetPhotoByPlaceId(placeId);

            var data = await _placeRepo.GetGovernmetsAsync();
            ViewBag.GovernmentList = new SelectList(data, "Id", "Name", place.GovernmentId);

            var model = new PlaceRemoveViewModel()
            {
                PlaceId = placeId,
                Name = place.Name,
                Address = place.Address,
                DateOfCreation = place.DateOfCreation,
                Details = place.Details,
                EntrancePrice = place.EntrancePrice,
                rate = place.rate,
                Photos = photos.ToList(),
                FirstImg = photos.ToList().FirstOrDefault().PhotoPath,
                GovernmentId = place.GovernmentId,
            };
            return View(model);
        }

        #endregion


        public IActionResult BookNow(int id)
        {
            _toast.AddSuccessToastMessage("A trip has been booked");
            return RedirectToAction("Index", "User");
        }

        public IActionResult ContactUsMobiles()
        {
            return View();
        }



    }
}
