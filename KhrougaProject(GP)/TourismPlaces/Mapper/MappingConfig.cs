using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TourismPlaces.Data;
using TourismPlaces.Data.Models;
using TourismPlaces.ViewModels;
using TourismPlaces.ViewModels.Account;
using TourismPlaces.ViewModels.Admin;

namespace TourismPlaces.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Add mapping here
            // CreateMap<src, des>().ReverseMap();
            //CreateMap<Place,PlaceViewModel>().ReverseMap();
            //CreateMap<Government, GovernmentViewModel>().ReverseMap();

            CreateMap<ApplicationUser, RegistrationViewModel>().ReverseMap();

            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();

            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();

            CreateMap<PlacesCreateViewModel, Place>()
                .ReverseMap();
            CreateMap<Photo,PhotoViewModel>().ReverseMap();

            CreateMap<Place, EditPlaceViewwModel>().ReverseMap();
            




        }
    }
}
