using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            var result = _db.Villas.ToList();

            return Ok(_mapper.Map<VillaDTO>(result));
        }

        [HttpGet("id", Name = "GetVilla")]
        //[Route("api/VillaAPI/GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villaObj = new VillaDTO();

            if (villaObj == null)
            {
                return NotFound();
            }
            var villa = _db.Villas.FirstOrDefault(x => x.Id == id);

            return Ok(villa);

        }

        [HttpPost]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] CreateVillaDTO createDTO)
        {
            if(createDTO == null)
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

            await _db.Villas.AddAsync(villa);
            await _db.SaveChangesAsync();
            
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public ActionResult DeleteVilla(int id)
        {
            var key = _db.Villas.FirstOrDefault(x => x.Id == id);
            if(key == null)
            {
                return NotFound();
            }
            else
            {
                _db.Villas.Remove(key);
                _db.SaveChanges();
            }
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public ActionResult UpdateVilla(int id, [FromBody]UpdateVillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            Villa villa = new Villa()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Amenity = villaDTO.Amenity,
                UpdateDate = DateTime.Now
            };

            var villaDb = _db.Villas.SingleOrDefault(x => x.Id == id);
            if(villaDb != null)
            {
                villa.CreatedDate = villaDb.CreatedDate;
            }

            _db.Villas.Update(villa);
            _db.SaveChanges();

            return Ok(villa);
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
