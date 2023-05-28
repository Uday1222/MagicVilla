using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModels;
using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Common;
using System.Data;
using System.Reflection;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private IVillaNumberService _villaNumberService;
        private IVillaService _villaService;
        private IMapper _mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<ActionResult> IndexVillaNumber()
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);

            var response = await _villaNumberService.GetAllAsync<APIResponse>(token);

            List<VillaNumberDTO> list = new List<VillaNumberDTO>();
            if(response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(response.Result.ToString());
            }
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateVillaNumber()
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.GetAllAsync<APIResponse>(token);

            VillaNumberCreateViewModel villaNumberCreateViewModel = new VillaNumberCreateViewModel();
            if (response != null && response.IsSuccess)
            {
                villaNumberCreateViewModel.villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }); ;
            }
            return View(villaNumberCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateVillaNumber(VillaNumberCreateViewModel model)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            if (ModelState.IsValid)
            {
                VillaNumberCreateDTO villaNumberCreateDTO = model.VillaNumber;
                var response = await _villaNumberService.CreateAsync<APIResponse>(villaNumberCreateDTO, token);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Number Created Successfully";
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else if(response.ErrorMessages.Count() > 0)
                {
                    TempData["error"] = response.ErrorMessages.FirstOrDefault();
                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                }

            }

            var res = await _villaService.GetAllAsync<APIResponse>(token);

            VillaNumberCreateViewModel villaNumberCreateViewModel = new VillaNumberCreateViewModel();
            if (res != null && res.IsSuccess)
            {
                villaNumberCreateViewModel.villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }); ;
            }
            return View(villaNumberCreateViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateVillaNumber(int villaId)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            VillaNumberUpdateViewModel villaNumberUpdateViewModel = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaId, token);

            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberUpdateViewModel.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(model);

                var res = await _villaService.GetAllAsync<APIResponse>(token);

                if (res != null && res.IsSuccess)
                {
                    villaNumberUpdateViewModel.villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString()).Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }); ;
                }
                return View(villaNumberUpdateViewModel);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateVillaNumber(VillaNumberUpdateViewModel model)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            if (ModelState.IsValid)
            {
                VillaNumberUpdateDTO villaNumberUpdateDTO = model.VillaNumber;
                var response = await _villaNumberService.UpdateAsync<APIResponse>(villaNumberUpdateDTO, token);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Number Updated Successfully";
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else if (response.ErrorMessages.Count() > 0)
                {
                    TempData["error"] = response.ErrorMessages.FirstOrDefault();

                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                }

            }

            var res = await _villaService.GetAllAsync<APIResponse>(token);

            VillaNumberUpdateViewModel villaNumberUpdateViewModel = new VillaNumberUpdateViewModel();
            if (res != null && res.IsSuccess)
            {
                villaNumberUpdateViewModel.villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }); ;
            }
            return View(villaNumberUpdateViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteVillaNumber(int villaId)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            VillaNumberDeleteViewModel villaNumberUpdateViewModel = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaId, token);

            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberUpdateViewModel.VillaNumber = _mapper.Map<VillaNumberDTO>(model);

                var res = await _villaService.GetAllAsync<APIResponse>(token);

                if (res != null && res.IsSuccess)
                {
                    villaNumberUpdateViewModel.villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString()).Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }); ;
                }
                return View(villaNumberUpdateViewModel);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteVillaNumber(VillaNumberDeleteViewModel model)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, token);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa Number Deleted Successfully";

                return RedirectToAction(nameof(IndexVillaNumber));
            }
            else
            {
                TempData["error"] = response.ErrorMessages.FirstOrDefault();
            }
            return View(model);
        }
    }
}
