using Database.DTO;
using Database.DTO.RoleDTO;
using Database.Models;
using Domain.IServices;

namespace Domain.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly RailwayTicketDbContext _railwayTicketDbContext;

        public UserRoleService(RailwayTicketDbContext railwayTicketDbContext)
        {
            _railwayTicketDbContext = railwayTicketDbContext;
        }

        public ResponseModel GetAllRole()
        {
            var roles = _railwayTicketDbContext.UserRoles.ToList();
            return new ResponseModel ("Successful.",true,roles);
        }

        public ResponseModel AddRole(RequestRoleDTO requestRoleDTO)
        {
            try
            {
                if(string.IsNullOrEmpty(requestRoleDTO.RoleName) || requestRoleDTO.RoleName == " ")
                {
                    return new ResponseModel("Enter valid role name", false);
                }
                var roleName = _railwayTicketDbContext.UserRoles.Where(x => x.Name == requestRoleDTO.RoleName).ToList();
                if(roleName.Count > 0)
                {
                    return new ResponseModel("Role name already exist.", false);
                }

                var role = new UserRole
                {
                    Name = requestRoleDTO.RoleName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                _railwayTicketDbContext.UserRoles.Add(role);
                _railwayTicketDbContext.SaveChanges();

                return new ResponseModel("Create role successful.", true, requestRoleDTO);
                    
            }
            catch(Exception ex)
            {
                return new ResponseModel(ex.Message, false);
            }
        }
    }
}
