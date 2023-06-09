﻿using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[controller]")] - this route will change for some reason
    // if we change the controller name, and the consumed UI also we need to change.
    // To avoid that, we just hardcode the route uri
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IMapper _mapper;
        public IVillaRepository villaRepository;
        protected APIResponse _apiResponse;

        public VillaAPIController(ILogger<VillaAPIController> logger, IMapper mapper, IVillaRepository villaRepository)
        {
            _logger = logger;
            _mapper = mapper;
            this.villaRepository = villaRepository;
            _apiResponse = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        //[ResponseCache(Duration = 30)] - this is for caching individual response
        [ResponseCache(CacheProfileName = "Default30")] // caching profile name should be same as declared in startup class caching profile
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetVillas([FromQuery(Name = "filterOccupancy")] int? occupancy,
            [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                List<Villa> result;
                if(occupancy > 0)
                {
                    result = await this.villaRepository.GetAll(x => x.Occupancy == occupancy, pageSize:pageSize, pageNumber:pageNumber);
                }
                else
                {
                    result = await this.villaRepository.GetAll(pageSize: pageSize, pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    result = result.Where(x => x.Name.ToLower().Contains(search)).ToList();
                }

                Pagination pagination = new ()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _apiResponse.Result = _mapper.Map<List<VillaDTO>>(result);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string> { ex.Message };
            }

            return Ok(_apiResponse);
        }

        [HttpGet("{id:int}",Name = "GetVilla")]
        //[Route("api/VillaAPI/GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                var villa = await villaRepository.Get(x => x.Id == id);
                if (villa == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.Result = _mapper.Map<VillaDTO>(villa);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string> { ex.Message };
            }
            return _apiResponse;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] CreateVillaDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest();
            }
            //if(villaDTO.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            //Villa villa = new Villa
            //{
            //    Name = createDTO.Name,
            //    Details = createDTO.Details,
            //    ImageUrl = createDTO.ImageUrl,
            //    Occupancy = createDTO.Occupancy,
            //    Rate = createDTO.Rate,
            //    Sqft = createDTO.Sqft,
            //    Amenity = createDTO.Amenity,
            //    CreatedDate = DateTime.Now,
            //    UpdateDate = DateTime.Now
            //};

            var villa = _mapper.Map<Villa>(createDTO);

            await villaRepository.CreateAsync(villa);

            _apiResponse.Result = _mapper.Map<VillaDTO>(villa);
            _apiResponse.StatusCode = HttpStatusCode.Created;
            _apiResponse.IsSuccess = true;

            return CreatedAtRoute("GetVilla", new { id = villa.Id }, _apiResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            var villa = await villaRepository.Get(x => x.Id == id);
            if (villa != null)
            {
                await villaRepository.RemoveAsync(villa);

                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                return Ok(_apiResponse);
            }
            else
            {
                return BadRequest();
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] UpdateVillaDTO villaUpdateDTO)
        {
            if (villaUpdateDTO == null || id != villaUpdateDTO.Id)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest();
            }

            //Villa villa = new Villa()
            //{
            //    Id = villaUpdateDTO.Id,
            //    Name = villaUpdateDTO.Name,
            //    Details = villaUpdateDTO.Details,
            //    ImageUrl = villaUpdateDTO.ImageUrl,
            //    Occupancy = villaUpdateDTO.Occupancy,
            //    Rate = villaUpdateDTO.Rate,
            //    Sqft = villaUpdateDTO.Sqft,
            //    Amenity = villaUpdateDTO.Amenity,
            //    UpdateDate = DateTime.Now
            //};

            var villa = _mapper.Map<Villa>(villaUpdateDTO);

            await villaRepository.Update(villa);

            _apiResponse.Result = _mapper.Map<VillaDTO>(villa);
            _apiResponse.StatusCode = HttpStatusCode.OK;

            return Ok(_apiResponse);
        }

        [HttpPatch("{id:int}", Name = "UpdateVilla")]
        public ActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            //AsNoTracking - In EF if we retrieve any record then EF will have track of it
            //               and if we want to update the same retrevied item, then it throws error.
            //               To avoid that we use AsNoTracking

            //db object
            VillaDTO villaDTO1 = new VillaDTO();

            patchDTO.ApplyTo(villaDTO1);

            return NoContent();
        }
    }
}
