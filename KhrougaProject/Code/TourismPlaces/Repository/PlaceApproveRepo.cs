using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TourismPlaces.Data;
using TourismPlaces.IRepository;
using TourismPlaces.ViewModels.Admin;

namespace TourismPlaces.Repository
{
    public class PlaceApproveRepo : IPlaceApproveRepo
    {
        private readonly ApplicationDbContext _context;

        public PlaceApproveRepo(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ApprovePostAsync(int placeId )
        {
            var place = await _context.Places.FindAsync(placeId);
            if (place == null) { return false; }
            place.IsApproved = true;
            _context.Places.Update(place);
            await _context.SaveChangesAsync();
            return true;
        }

        public IEnumerable<AdminApprovePlaceViewModel> GetApprovedPlaces()
        {
            var places = _context.Places.Include(c => c.Government).Include(c=>c.ApplicationUser).Where(p=>p.IsApproved==false);
            //var x = _context.Users.Include(c => c.Places).ToList();
            List<AdminApprovePlaceViewModel> placesList = new List<AdminApprovePlaceViewModel>();
            foreach (var place in places)
            {

                var model = new AdminApprovePlaceViewModel()
                {
                    CovernmentName = place.Government.Name,
                    EntryPrice = place.EntrancePrice,
                    PlaceName = place.Name,
                    Rate = place.rate,
                    Owner=place.ApplicationUser.UserName,
                    IsApproved=place.IsApproved,
                    PlaceId=place.Id
                };
                placesList.Add(model);

            }
            return placesList;
        }
    }
}
