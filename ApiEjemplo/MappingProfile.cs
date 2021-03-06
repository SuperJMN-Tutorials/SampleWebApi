﻿using ApiEjemplo.Features.Activities;
using ApiEjemplo.Features.Bikes;
using ApiEjemplo.Features.Users;
using ApiEjemplo.Model;
using AutoMapper;
using BikingUltimate.Server.Features.Users;
using GetAll = BikingUltimate.Server.Features.Users.GetAll;

namespace ApiEjemplo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Activity, ActivityRead>();
            CreateMap<Component, GetAllComponents.ComponentRead>();
            CreateMap<CreateComponent.ComponentInfo, Component>();
            CreateMap<User, GetAll.UserRead>();
            CreateMap<Bike, GetAllBikes.BikeRead>();
            CreateMap<CreateBike.BikeInfo, Bike>();
            CreateMap<User, GetUsersWhoRequireMaintence.MaintanceUserRead>();
        }
    }
}