using AutoMapper;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, CreateVillaDTO>().ReverseMap();
            CreateMap<VillaDTO, UpdateVillaDTO>().ReverseMap();

            CreateMap<List<VillaDTO>, VillaDTO>();

            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();

            CreateMap<List<VillaNumberDTO>, VillaNumberDTO>();

            //CreateMap<VillaNumberDTO, VillaNumberUpdateViewModel>().ReverseMap();
            //CreateMap<VillaNumberDTO, VillaNumberCreateViewModel>().ReverseMap();
        }
    }
}
