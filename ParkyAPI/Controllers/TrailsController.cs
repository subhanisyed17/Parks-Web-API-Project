using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.models;
using ParkyAPI.models.DTOs;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    //[Route("api/Trails")]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiVersion("1.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecTrails")]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepsoitory _trailRepo;

        private readonly IMapper _mapper;

        public TrailsController(ITrailRepsoitory trailRepo, IMapper mapper)
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of All Trails
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<TrailDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails()
        {
            var trail = _trailRepo.GetTrails();
            var objDto = new List<TrailDTO>();

            foreach (var t in trail)
            {
                objDto.Add(_mapper.Map<TrailDTO>(t));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Get Individual Trail
        /// </summary>
        /// <param name="trailId">Id of the Trail</param>
        /// <returns></returns>

        [HttpGet("{trailId:int}", Name ="GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepo.GetTrail(trailId);
            var objDto = _mapper.Map<TrailDTO>(trail);
            if (objDto == null)
                return NotFound();
            return Ok(objDto);

        }

        [HttpGet("[action]/{nationalParkId:int}", Name = "GetTrailInNationalPark")]
        [ProducesResponseType(200, Type = typeof(TrailDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetTrailInNationalPark(int nationalParkId)
        {
            var trailList = _trailRepo.GetTrailsInNationalParkId(nationalParkId);
            if (trailList == null)
                return NotFound();

            var objDto = new List<TrailDTO>();
            foreach (var trail in trailList)
            {
                objDto.Add(_mapper.Map<TrailDTO>(trail)); ;
            }
            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type=typeof(TrailDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateTrail([FromBody] TrailCreateDTO trailDto)
        {
            if (trailDto == null)
                return BadRequest(ModelState);

            if (_trailRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", $"{trailDto.Name} Already Exists");
                return StatusCode(404,ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var trail = _mapper.Map<Trail>(trailDto);

            if(!_trailRepo.CreateTrail(trail))
            {
                ModelState.AddModelError("",$"Something went wrong while saving the trail details to database");
                return StatusCode(500,ModelState);
            }

            return CreatedAtRoute("GetTrail", new {trailId = trail.Id}, trail);
        }

        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDTO trailDTO)
        {
            if (trailDTO == null || trailId != trailDTO.Id)
                return BadRequest(ModelState);

            var trail = _mapper.Map<Trail>(trailDTO);

            if(!_trailRepo.UpdateTrail(trail))
            {
                ModelState.AddModelError("",$"Something Went wrong while updating the {trailDTO.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepo.TrailExists(trailId))
                return NotFound();

            var trail = _trailRepo.GetTrail(trailId);

            if (!_trailRepo.DeleteTrail(trail))
            {
                ModelState.AddModelError("",$"Something Went wrong while deleting the {trail.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}