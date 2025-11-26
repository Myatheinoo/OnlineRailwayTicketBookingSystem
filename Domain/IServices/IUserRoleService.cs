using Database.DTO;
using Database.DTO.RoleDTO;

namespace Domain.IServices
{
    public interface IUserRoleService
    {
        public ResponseModel GetAllRole();
        public ResponseModel AddRole(RequestRoleDTO requestRoleDTO);
    }
}
