using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using TourismPlaces.Data;
using TourismPlaces.Data.Models;
using TourismPlaces.Helper;
using TourismPlaces.IRepository;
using TourismPlaces.ViewModels;

namespace TourismPlaces.Repository
{
    public class PlaceRepo : IPlaceRepo
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public PlaceRepo(IMapper mapper, ApplicationDbContext context,SignInManager<ApplicationUser> signInManager, IHttpContextAccessor  httpContextAccessor,UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task CreatPlace(PlacesCreateViewModel placeView)
        {
            try
            {
                string userId = string.Empty;
                var currentUser = httpContextAccessor.HttpContext.User;
                if (_signInManager.IsSignedIn(currentUser) && currentUser.IsInRole("owner"))
                {
                   userId= userManager.GetUserId(currentUser);
                }

                var place = new Place()
                {
                    Name = placeView.Name,
                    Address = placeView.Address,
                    DateOfCreation = DateTime.Now,
                    EntrancePrice = placeView.EntrancePrice,
                    Details = placeView.Details,
                    GovernmentId = placeView.GovernmentId,
                    rate = placeView.rate,
                    IsDeleted = !placeView.IsDeleted ,
                    UserId=userId

                };


                await _context.Places.AddAsync(place);
                await _context.SaveChangesAsync();

                if (placeView.photos != null)
                {
                    foreach (var attach in placeView.photos)
                    {
                        Photo photo = new Photo()
                        {
                            PhotoPath = DocumentSetting.UploadFile(attach, "photos"),
                            PlaceId = place.Id
                        };
                        _context.photos.Add(photo);
                        await _context.SaveChangesAsync();

                    }

                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public IEnumerable<PlacesRetrieveViewModel> GetAllPlaces()
        {
            var placesRetrieveViewModelList = new List<PlacesRetrieveViewModel>();
            var places = _context.Places.Include(p=>p.Government).ToList();
            foreach (var place in places)
            {
                var photosNumber = _context.photos.Where(x => x.PlaceId == place.Id).ToList().Count;
                var placesRetrieveViewModel = new PlacesRetrieveViewModel()
                {
                    PlaceId=place.Id,
                    Name = place.Name,
                    Address = place.Address,
                    EntrancePrice = place.EntrancePrice,
                    DateOfCreation = place.DateOfCreation,
                    GovernmentName = place.Government.Name,
                    rate =place.rate,
                    PhotosNumber = photosNumber,
                    IsApproved = place.IsApproved,
                    IsDeleted = place.IsDeleted,
                    
                };
                placesRetrieveViewModelList.Add(placesRetrieveViewModel);
            }
            return placesRetrieveViewModelList;
        }

        public async Task<IEnumerable<Government>> GetGovernmetsAsync()
        {
            return await _context.Governments.ToListAsync();
        }

        public IEnumerable<PhotoViewModel> GetPhotoByPlaceId(int placeId)
        {
           var photos= _context.photos.Where(p=>p.PlaceId==placeId);
            return _mapper.Map<List<PhotoViewModel>>(photos);
        }

        public async Task<Place> GetPlaceByIdAsync(int placeId)
        {
            return await _context.Places.FindAsync(placeId);
        }

        public async Task GoPlaceLiveAsync(int placeId)
        {
            var place =await _context.Places.FirstOrDefaultAsync(p => p.Id == placeId);
            if (place.IsDeleted)
            {
                place.IsDeleted = false;
                _context.Places.Update(place);
            }
            else
            {
                place.IsDeleted = true;
                _context.Places.Update(place);
            }
           
            await _context.SaveChangesAsync();
        }


        public async Task AddMoreImagesAsync(int placeId, List<IFormFile> MorePhotos)
        {
            if (MorePhotos != null)
            {
                foreach (var attach in MorePhotos)
                {
                    Photo photo = new Photo()
                    {
                        PhotoPath = DocumentSetting.UploadFile(attach, "photos"),
                        PlaceId = placeId
                    };
                    _context.photos.Add(photo);
                    await _context.SaveChangesAsync();

                }

            }
        }

        //remove Photo from Db and FileServer
        public async Task<bool> RemovePhotoAsync(List<PhotoSelectViewModel> RemovePhotos)
        {
          
            try
            {
                if (RemovePhotos == null)
                {
                    return false;
                }
                for (int i = 0; i < RemovePhotos.Count; i++)
                {
                    if (!RemovePhotos[i].IsPhotoSelected)
                    {
                        //remove From Db
                        var photoToRemove = await _context.photos.FindAsync(RemovePhotos[i].Id);
                        _context.photos.Remove(photoToRemove);
                        await _context.SaveChangesAsync();

                        //remove from file Server
                        DocumentSetting.DeleteFile(RemovePhotos[i].PhotoPath, "photos");
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public async Task UpdatePlaceAsync(EditPlaceViewwModel model, int PlaceId)
        {
            var place =await _context.Places.FindAsync(PlaceId);
            if (place==null)
            {
                
            }


            place.EntrancePrice = model.EntrancePrice;
            place.Address = model.Address;
            place.rate = model.rate;
            place.Details = model.Details;
            place.GovernmentId = model.GovernmentId;
            place.IsDeleted = !model.IsDeleted;
            place.Name = model.Name;

            _context.Places.Update(place);
            await _context.SaveChangesAsync();

            for (int i = 0; i < model.Photos.Count; i++)
            {
                //if photo is unchecked delete it from db and file server
                //else do nothing
                if (!model.Photos[i].IsPhotoSelected)
                {
                    var result = await RemovePhotoAsync(model.Photos);
                }
            }
            //add new photos if exists
            if (model.PhotosToAdd != null)
            {
                await AddMoreImagesAsync(PlaceId, model.PhotosToAdd);

            }
        }

        //**********************************************************************************
        public async Task<IEnumerable<PlacesRetrieveViewModelForUser>> GetAllPlacesInGov(int governmentId)
        {
            var places = await _context.Places
                .Include(p => p.Government)
                        .Include(p => p.Photos)
                .Where(p => p.GovernmentId == governmentId && !p.IsDeleted && p.IsApproved)
                .ToListAsync();

            var placesRetrieveViewModelListForUser = new List<PlacesRetrieveViewModelForUser>();

            foreach (var place in places)
            {
                //  var photosNumber = _context.photos.Where(x => x.PlaceId == place.Id).ToList().Count;
                var placesRetrieveViewModelForUser = new PlacesRetrieveViewModelForUser()
                {
                    Id = place.Id,
                    Name = place.Name,
                    Address = place.Address,
                    EntrancePrice = place.EntrancePrice,
                    GovernmentName = place.Government.Name,
                    Rate = place.rate,
                    //PhotosNumber = photosNumber,
                    //IsApproved = place.IsApproved,
                    //  IsDeleted = place.IsDeleted,
                    PhotoPath = place.Photos.FirstOrDefault(p => p.PlaceId == place.Id).PhotoPath

                };
                placesRetrieveViewModelListForUser.Add(placesRetrieveViewModelForUser);
            }

            return placesRetrieveViewModelListForUser;
        }

        public async Task<IEnumerable<PlacesRetrieveViewModelForUser>> GetAllPlacesForUser()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.Users.FindAsync(userId);

            var places = _context.Places.Include(p => p.Government)
                .Include(p => p.Photos)
                .Where(p => p.GovernmentId == user.GovernmentId).ToList();

            var placesRetrieveViewModelForUserList = new List<PlacesRetrieveViewModelForUser>();


            foreach (var place in places)
            {
                var placesRetrieveViewModelForUser = new PlacesRetrieveViewModelForUser()
                {
                    Id = place.Id,
                    Name = place.Name,
                    Address = place.Address,
                    EntrancePrice = place.EntrancePrice,
                    Rate = place.rate,
                    Details = place.Details,
                    GovernmentName = place.Government.Name,
                    UserId = userId,
                    PhotoPath = place.Photos.FirstOrDefault(p => p.PlaceId == place.Id).PhotoPath
                };
                placesRetrieveViewModelForUserList.Add(placesRetrieveViewModelForUser);
            };


            return placesRetrieveViewModelForUserList;
        }

        public async Task<Government> GetGovernmentByIdAsync(int governmentId)
        {
            var government = await _context.Governments.FindAsync(governmentId);
            return government;
        }
    }
}
