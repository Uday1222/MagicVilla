﻿using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, CreateVillaDTO>().ReverseMap();
            CreateMap<Villa, UpdateVillaDTO>().ReverseMap();

        }
    }
}
