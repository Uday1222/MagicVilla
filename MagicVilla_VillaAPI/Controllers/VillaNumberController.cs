using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaNumber")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _repository;
        private readonly IVillaRepository _villaRepository;
        protected APIResponse _apiResponse;

        public VillaNumberController(IMapper mapper, IVillaNumberRepository repository, IVillaRepository villaRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _apiResponse = new();
            _villaRepository = villaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetVillas()
        {
            try
            {
                var result = await this._repository.GetAll(includeProperties: "Villa");

                _apiResponse.Result = _mapper.Map<List<VillaNumberDTO>>(result);
                _apiResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string> { ex.Message };
            }

            return Ok(_apiResponse);
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        //[Route("api/VillaAPI/GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                var villa = await _repository.Get(x => x.VillaNo == id, includeProperties: "Villa");
                if (villa == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.Result = _mapper.Map<VillaNumberDTO>(villa);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                var villaId = await _villaRepository.Get(x => x.Id == createDTO.VillaID);
                if(villaId == null)
                {
                    ModelState.AddModelError("ErrorMessages", "The number is invalid");
                    return BadRequest(ModelState);
                }
                if(await _repository.Get(x => x.VillaNo == createDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Number already exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest();
                }

                var villa = _mapper.Map<VillaNumber>(createDTO);
                villa.CreatedDate = DateTime.Now;
                villa.UpdatedDate = DateTime.Now;
                await _repository.CreateAsync(villa);

                _apiResponse.Result = _mapper.Map<VillaNumberDTO>(villa);
                _apiResponse.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVillaNumber", new { id = villa.VillaNo }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.Created;
                _apiResponse.ErrorMessages = new List<string> { ex.Message };
                _apiResponse.IsSuccess = false;
            }
            return _apiResponse;

        }

        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                var villa = await _repository.Get(x => x.VillaNo == id);
                if (villa != null)
                {
                    await _repository.RemoveAsync(villa);

                    _apiResponse.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_apiResponse);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.Created;
                _apiResponse.ErrorMessages = new List<string> { ex.Message };
                _apiResponse.IsSuccess = false;
            }
            return _apiResponse;
        }

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaNumberUpdateDTO villaUpdateDTO)
        {
            try
            {
                if (villaUpdateDTO == null || id != villaUpdateDTO.VillaNo)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var villaId = await _villaRepository.Get(x => x.Id == villaUpdateDTO.VillaID);
                if (villaId == null)
                {
                    ModelState.AddModelError("ErrorMessages", "The number is invalid");
                    return BadRequest(ModelState);
                }

                var villa = _mapper.Map<VillaNumber>(villaUpdateDTO);

                await _repository.Update(villa);

                _apiResponse.Result = _mapper.Map<VillaNumberDTO>(villa);
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.Created;
                _apiResponse.ErrorMessages = new List<string> { ex.Message };
                _apiResponse.IsSuccess = false;
            }
            return _apiResponse;


        }

    }
}
