using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Collections.Generic;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private IVillaService _villaService;
        private IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<ActionResult> IndexVilla()
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.GetAllAsync<APIResponse>(token);

            List<VillaDTO> list = new();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> CreateVilla(CreateVillaDTO model)
        {
            if (ModelState.IsValid)
            {
                string token = HttpContext.Session.GetString(SD.SessionToken);
                var response = await _villaService.CreateAsync<APIResponse>(model, token);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateVilla(int villaId)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.GetAsync<APIResponse>(villaId, token);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<UpdateVillaDTO>(model));
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateVilla(UpdateVillaDTO model)
        {
            if (ModelState.IsValid)
            {
                string token = HttpContext.Session.GetString(SD.SessionToken);
                var response = await _villaService.UpdateAsync<APIResponse>(model, token);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteVilla(int villaId)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.GetAsync<APIResponse>(villaId, token);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteVilla(VillaDTO model)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.DeleteAsync<APIResponse>(model.Id, token);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CheckValue(int val)
        //{
        //    //bool isUnique = !db.Records.Any(r => r.Value == value && r.ID != recordID);
        //    //return Json(isUnique);

        //    return RedirectToAction("CreateVilla");
        //}
    }
}
