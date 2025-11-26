using Database.DTO.RegionDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }
        [HttpGet("GetAllRegions")]
        public IActionResult GetAllRegions()
        {

            var result = _regionService.GetAllRegions();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AddRegion")]
        public IActionResult AddRegion(RegionDTO newRegion)
        {
            var result = _regionService.AddRegion(newRegion);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("UpdateRegion")]
        public IActionResult UpdateRegion(UpdateRegionDTO newRegion)
        {
            var result = _regionService.UpdateRegion(newRegion);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AddTownship")]
        public IActionResult AddTownship(TownshipDTO newTownship)
        {
            var result = _regionService.AddTownship(newTownship);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllTownships")]
        public IActionResult GetAllTownships()
        {

            var result = _regionService.GetAllTownships();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("UpdateTownship")]
        public IActionResult UpdateTownship(UpdateTownshipDTO newTownship)
        {
            var result = _regionService.UpdateTownship(newTownship);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
