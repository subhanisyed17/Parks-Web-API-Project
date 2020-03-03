using AutoMapper;
using ParkyAPI.models;
using ParkyAPI.models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Mapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
            CreateMap<Trail, TrailDTO>().ReverseMap();
            CreateMap<Trail, TrailUpdateDTO>().ReverseMap();
            CreateMap<Trail, TrailCreateDTO>().ReverseMap();
        }
    }
}
