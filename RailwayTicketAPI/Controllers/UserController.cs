using Database.DTO.UserDTO;
using DocumentFormat.OpenXml.Presentation;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin,Check Ticket")]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserRequestModel user)
        {
            var result = await _userService.AddUser(user);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers() {
            {
                var result = _userService.GetAllUser();
                if (result.IsSuccess)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result);
            }
        }

        [HttpPost("GetByEachUser")]
        public IActionResult GetByEachUser(int id)
        {
            var result = _userService.GetByEachUser(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserRequestModel requestModel)
        {
            var result = _userService.UpdateUser(requestModel);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequestModel loginModel)
        {
            var result = await _userService.UserLogin(loginModel);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
