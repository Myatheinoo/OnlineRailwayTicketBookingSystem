using Database.DTO;
using Database.DTO.RolePermissionDTO;

namespace Domain.IServices
{
    public interface IPermissionServices 
    {
        public ResponseModel AddPermission(string permissionName,string guardName);
        public ResponseModel AddRolePermission(RolePermission rolePermission);
        public ResponseModel GetPermissionByRoleId(int roleId);
        public ResponseModel UpdateRolePermission(RolePermission rolePermission);
    }
}
