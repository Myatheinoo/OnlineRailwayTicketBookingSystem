using Database.DTO.CarriageDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiVersion("1.0")]

    public class CarriageController : ControllerBase
    {
        private readonly ICarriageService _carriageService;

        public CarriageController(ICarriageService carriageService)
        {
            _carriageService = carriageService;
        }
        [HttpGet("GetAllCarriage")]
        public async Task<IActionResult> GetAllCarriage()
        {
            var result = await _carriageService.GetAllCarriage();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddCarriage")]
        public IActionResult AddCarriage(CarriageDTO newCarriage)
        {
            var result = _carriageService.AddCarriage(newCarriage);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateCarriageById")]
        public IActionResult UpdateCarriageById(UpdateCarriageDTO newData)
        {
            var result = _carriageService.UpdateCarriageById(newData);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
