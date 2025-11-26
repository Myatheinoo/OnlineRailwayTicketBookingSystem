using Database.DTO.StationDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet("GetAllStations")]
        public async Task<IActionResult> GetAllStations()
        {
            var result = await _stationService.GetAllStation();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetStationById")]
        public async Task<IActionResult> GetStationById(int id)
        {
            var result = await _stationService.GetStationById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddNewStation")]
        public async Task<IActionResult> AddNewStation(StationDTO newStation)
        {
            var result = await _stationService.AddNewStation(newStation);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateStation")]
        public async Task<IActionResult> UpdateStation(UpdateStationDTO updateStationDTO)
        {
            var result = await _stationService.UpdateStation(updateStationDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
