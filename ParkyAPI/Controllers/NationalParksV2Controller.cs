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
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    public class NationalParksV2Controller : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepo;

        private readonly IMapper _mapper;

        public NationalParksV2Controller(INationalParkRepository nationalParkRepo, IMapper mapper)
        {
            _nationalParkRepo = nationalParkRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of All National Parks
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(NationalParkDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var nationalPark = _nationalParkRepo.GetNationalParks().FirstOrDefault();
            //var objDto = new NationalParkDTO();
            return Ok(_mapper.Map<NationalParkDTO>(nationalPark));
        }

    }
}