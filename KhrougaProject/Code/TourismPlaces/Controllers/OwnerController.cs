using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System.Data;
using TourismPlaces.IRepository;
using TourismPlaces.ViewModels;

namespace TourismPlaces.Controllers
{
    [Authorize(Roles = "owner")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class OwnerController : Controller
    {
        private readonly IPlaceRepo _placeRepo;
        private readonly IToastNotification _toast;

        public OwnerController(IPlaceRepo placeRepo , IToastNotification toast)
        {
            _placeRepo = placeRepo;
            _toast = toast;
        }

        #region All Places
        [HttpGet]
        
        public ActionResult Index()
        {
            return View(_placeRepo.GetAllPlaces());
        }

        #endregion



        #region ADD PLACE
        [HttpGet]
        public async Task<IActionResult> AddPlace()
        {
            var data = await _placeRepo.GetGovernmetsAsync();
            ViewBag.GovernmentList = new SelectList(data, "Id", "Name");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddPlace(PlacesCreateViewModel model)
        {
            try
            {
                var data = await _placeRepo.GetGovernmetsAsync();
                ViewBag.GovernmentList = new SelectList(data, "Id", "Name");
                await _placeRepo.CreatPlace(model);
                _toast.AddSuccessToastMessage("Place created successfully");
                return RedirectToAction("Index", "Owner");
            }
            catch (Exception)
            {
                _toast.AddErrorToastMessage("Something went wrong");
                return View(model);
            }
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


        #region GO LIVE
        public async Task<IActionResult> CheckLive(int placeId)
        {
            try
            {
                await _placeRepo.GoPlaceLiveAsync(placeId);
                _toast.AddSuccessToastMessage("Visability of place has chenged");
                return RedirectToAction("Index", "Owner");
            }
            catch (Exception)
            {
                _toast.AddErrorToastMessage("Something went wrong ! please try again later");
                return RedirectToAction("Index", "Owner");
            }

        }
        #endregion


        #region UPDATE PLACE
        [HttpGet]
        public async Task<IActionResult> EditPlace(int placeId)
        {
            var place = await _placeRepo.GetPlaceByIdAsync(placeId);
            var photos = _placeRepo.GetPhotoByPlaceId(placeId);

            var data = await _placeRepo.GetGovernmetsAsync();
            ViewBag.GovernmentList = new SelectList(data, "Id", "Name", place.GovernmentId);

          
            List<PhotoSelectViewModel> imgs = new List<PhotoSelectViewModel>();
            foreach (var img in photos)
            {
                var PhotoViewModel = new PhotoSelectViewModel()
                {
                    Id=img.Id,
                    PhotoPath=img.PhotoPath,
                    IsPhotoSelected = true
                };
               
                imgs.Add(PhotoViewModel);
            }    
            var model = new EditPlaceViewwModel()
            {
                PlaceId = placeId,
                Name = place.Name,
                Address = place.Address,
                DateOfCreation = place.DateOfCreation,
                Details = place.Details,
                EntrancePrice = place.EntrancePrice,
                rate = place.rate,
                Photos=imgs,
                GovernmentId = place.GovernmentId,
                IsDeleted = !place.IsDeleted
            };
            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> EditPlace(EditPlaceViewwModel model, int PlaceId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _toast.AddErrorToastMessage("Something Invaild");
                                       return View(model);
                }

                await _placeRepo.UpdatePlaceAsync(model, PlaceId);
                _toast.AddSuccessToastMessage("Place updated successfully");
                return RedirectToAction("PlaceDetails", "Owner", new { placeId = PlaceId });
            }
            catch (Exception)
            {

                _toast.AddErrorToastMessage("Something went wrong ! please try again later");
                return RedirectToAction("Index", "Owner");
            }

        }
        #endregion



        [HttpPost]
        public async Task<IActionResult> AddMorePhotos(int placeId,List<IFormFile> MorePhotos)
        {
          
            try
            {
                await _placeRepo.AddMoreImagesAsync(placeId, MorePhotos);
                _toast.AddSuccessToastMessage("New photos were added Successfully");
                return RedirectToAction("Index", "Owner");
            }
            catch (Exception)
            {
                _toast.AddErrorToastMessage("Something went wrong ! please try again later");
                return RedirectToAction("Index", "Owner");
               
            }

        }



        [HttpGet]
        public IActionResult OwnerDashboard()
        {
            return RedirectToAction("ChartsForOwner", "Dashboard");
        }
        
    }
}
