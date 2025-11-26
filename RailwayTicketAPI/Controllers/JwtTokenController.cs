using Azure;
using Database.DTO.JwtTokenDTO;
using Database.DTO.UserDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtTokenController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public JwtTokenController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> GenerateRefreshtoken([FromBody]RefreshRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest("Invalid token");
            }

            var result = await _jwtTokenService.ValidateRefreshToken(request.Token);
            if(result != null)
            {
                return Ok(result);
            }
            return Unauthorized("Invalid refresh token");
        }
    }
}
