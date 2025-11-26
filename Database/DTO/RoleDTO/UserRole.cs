using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.RoleDTO
{
    public class UserRoleDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class RequestRoleDTO
    {
        public string? RoleName { get; set; }
    }
}
