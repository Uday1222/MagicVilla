﻿using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private IVillaService _villaService;
        private IMapper _mapper;

        public HomeController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var response = await _villaService.GetAllAsync<APIResponse>();

            List<VillaDTO> list = new();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}