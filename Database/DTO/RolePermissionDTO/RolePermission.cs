using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.RolePermissionDTO
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<RolePermissionData>? RolePermissionInfo{ get; set; }

    }
    public class ResponseRolePermission
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<RolePermissionsData>? RolePermissionResponse { get; set; }
    }

    public class RolePermissionData
    {
        public int PermissionId { get; set; }
        public bool IsChecked { get; set; }
    }
    public class RolePermissionsData
    {
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public bool IsChecked { get; set; }
    }
}
