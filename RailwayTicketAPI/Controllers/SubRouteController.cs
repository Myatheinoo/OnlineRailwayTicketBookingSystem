using Database.DTO;
using Database.DTO.SubRouteDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubRouteController : ControllerBase
    {
        private readonly ISubRouteService _subRouteService;

        public SubRouteController(ISubRouteService subRouteService)
        {
            _subRouteService = subRouteService;
        }

        [HttpGet("GetAllSubRoute")]
        public async Task<IActionResult> GetAllRoute()
        {
            var result = await _subRouteService.GetAllSubRoute();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddSubRoute")]
        public async Task<IActionResult> AddSubRoute(SubRouteDTO newSubRoute)
        {
            var result =  await _subRouteService.AddSubRoute(newSubRoute);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
