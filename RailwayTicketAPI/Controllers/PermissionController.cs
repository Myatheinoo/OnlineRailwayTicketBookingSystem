using Database.DTO.RolePermissionDTO;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionServices _permissionServices;

        public PermissionController(IPermissionServices permissionServices)
        {
            _permissionServices = permissionServices;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("AddPermission")]
        public IActionResult AddPermission(string permissionName,string guardName)
        {
            var result = _permissionServices.AddPermission(permissionName, guardName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddRolePermission")]
        public IActionResult AddRolePermission(RolePermission rolePermission)
        {
            var result = _permissionServices.AddRolePermission(rolePermission);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetPermissionByRoleId")]
        public IActionResult GetPermissionByRoleId(int roleId)
        {
            var result = _permissionServices.GetPermissionByRoleId(roleId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateRolePermission")]
        public IActionResult UpdateRolePermission(RolePermission rolePermission)
        {
            var result = _permissionServices.UpdateRolePermission(rolePermission);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
