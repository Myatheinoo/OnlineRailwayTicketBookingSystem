using Database.DTO.MainRouteDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainRouteController : ControllerBase
    {
        private readonly IMainRouteService _mainRouteService;

        public MainRouteController(IMainRouteService mainRouteService)
        {
            _mainRouteService = mainRouteService;
        }

        [HttpGet("GetAllMainRoutes")]
        public async Task<IActionResult> GetAllMainRoutes()
        {
            var result = await _mainRouteService.GetAllMainRoutes();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddMainRoute")]
        public async Task<IActionResult> AddMainRoute(MainRouteDTO newMainRoute)
        {
            var result = await _mainRouteService.AddMainRoute(newMainRoute);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateMainRouteById")]
        public async Task<IActionResult> UpdateMainRouteById( UpdateMainRouteDTO routeData)
        {
            var result = await _mainRouteService.UpdateMainRouteById(routeData);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
