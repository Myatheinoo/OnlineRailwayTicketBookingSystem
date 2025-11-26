using Database.DTO.RoleDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _roleService;

        public UserRoleController(IUserRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRole()
        {
            var result = _roleService.GetAllRole();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("AddRole")]
        public IActionResult AddRole(RequestRoleDTO roleDTO)
        {
            var result = _roleService.AddRole(roleDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
