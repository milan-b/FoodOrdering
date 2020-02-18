using AutoMapper;
//using WebApi.Entities;
using WebApi.Models.Users;
using Domain.Models;
using WebApi.ViewModels;
using System.Collections.Generic;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();

            CreateMap<Meni, MeniViewModel>();
            CreateMap<Meni, MeniForCalendarViewModel>();
            CreateMap<Hrana, HranaViewModel>();
            CreateMap<HranaMeni, HranaMeniViewModel>();
            CreateMap<HranaPrilog, HranaPrilogViewModel>();
            CreateMap<Prilog, PrilogViewModel>();
        }
    }
}