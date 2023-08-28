using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestueantDB.Models;
using RestueantDB.ModelViews;

namespace RestueantDB.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Resturant, ResturantMV>().ReverseMap();
            CreateMap<Meal,MealMV >().ReverseMap();
          

        }
    }
}
