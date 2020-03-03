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
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiVersion("1.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepo;

        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository nationalParkRepo, IMapper mapper)
        {
            _nationalParkRepo = nationalParkRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of All National Parks
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<NationalParkDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var nationalParks = _nationalParkRepo.GetNationalParks();
            var objDto = new List<NationalParkDTO>();

            foreach (var nPark in nationalParks)
            {
                objDto.Add(_mapper.Map<NationalParkDTO>(nPark));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Get Individual National Park
        /// </summary>
        /// <param name="nationalParkId">Id of the National Park</param>
        /// <returns></returns>

        [HttpGet("{nationalParkId:int}", Name ="GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _nationalParkRepo.GetNationalPark(nationalParkId);
            var objDto = _mapper.Map<NationalParkDTO>(nationalPark);
            if (objDto == null)
                return NotFound();
            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type=typeof(NationalParkDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateNationalPark([FromBody] NationalParkDTO nationalParkDto)
        {
            if (nationalParkDto == null)
                return BadRequest(ModelState);

            if (_nationalParkRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", $"{nationalParkDto.Name} Already Exists");
                return StatusCode(404,ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if(!_nationalParkRepo.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("",$"Something went wrong while saving the national park details to database");
                return StatusCode(500,ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new {nationalParkId = nationalPark.Id}, nationalPark);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDTO nationalParkDTO)
        {
            if (nationalParkDTO == null || nationalParkId != nationalParkDTO.Id)
                return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDTO);

            if(!_nationalParkRepo.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("",$"Something Went wrong while updating the {nationalParkDTO.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepo.NationalParkExists(nationalParkId))
                return NotFound();

            var nationalPark = _nationalParkRepo.GetNationalPark(nationalParkId);

            if (!_nationalParkRepo.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("",$"Something Went wrong while deleting the {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}