using Database.DTO;
using Database.DTO.RolePermissionDTO;
using Database.DTO.Shared;
using Database.Models;
using Domain.IServices;

namespace Domain.Services
{
    public class PermissionService : IPermissionServices
    {
        private readonly RailwayTicketDbContext _railwayTicketDbContext;

        public PermissionService(RailwayTicketDbContext railwayTicketDbContext)
        {
            _railwayTicketDbContext = railwayTicketDbContext;
        }

        public ResponseModel AddPermission(string permissionName, string guardName) 
        {
            try
            {
                var permission = _railwayTicketDbContext.Permissions.FirstOrDefault(x => x.Name.ToLower().Trim() == permissionName.ToLower().Trim());
                if (permission is null)
                {
                    var newPermission = new Permission 
                    {
                        Name = permissionName,
                        GuardName = guardName,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _railwayTicketDbContext.Permissions.Add(newPermission);
                    _railwayTicketDbContext.SaveChanges();
                    return new ResponseModel(MessageModel.AddSuccess, true, permissionName);
                }
                return new ResponseModel(MessageModel.UnSuccess, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel AddRolePermission(RolePermission rolePermission)
        {
            var PermissionList = new List<RoleHasPermission>();
            var role = _railwayTicketDbContext.UserRoles.FirstOrDefault(x => x.RoleId == rolePermission.RoleId);
            if (role != null && rolePermission.RolePermissionInfo != null)
            {
                foreach (var item in rolePermission.RolePermissionInfo)
                {
                    var permission = new RoleHasPermission
                    {
                        RoleId = role.RoleId,
                        PermissionId = item.PermissionId,
                        IsChecked = item.IsChecked,
                    };
                    PermissionList.Add(permission);
                }
                _railwayTicketDbContext.RoleHasPermissions.AddRange(PermissionList);
                _railwayTicketDbContext.SaveChangesAsync();
            }
            return new ResponseModel(MessageModel.Success, true);
        }
        public ResponseModel GetPermissionByRoleId(int roleId)
        {
            try
            {
                var role = _railwayTicketDbContext.UserRoles.Where(x => x.RoleId == roleId).FirstOrDefault();
                if (role != null)
                {
                    var rolePermissions = (
                                                from rhp in _railwayTicketDbContext.RoleHasPermissions
                                                join p in _railwayTicketDbContext.Permissions on rhp.PermissionId equals p.Id
                                                where rhp.RoleId == roleId
                                                select new RolePermissionsData
                                                {
                                                    PermissionId = rhp.PermissionId,
                                                    PermissionName = p.Name,
                                                    IsChecked = rhp.IsChecked
                                                }
                                            ).ToList();

                    var rolePermissionList = new ResponseRolePermission
                    {
                        RoleId = role.RoleId,
                        RoleName = role.Name,
                        RolePermissionResponse = rolePermissions
                    };
                    return new ResponseModel(MessageModel.Success, true, rolePermissionList);
                }
                return new ResponseModel(MessageModel.UnSuccess, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ResponseModel UpdateRolePermission(RolePermission rolePermission)
        {
            var PermissionList = new List<RoleHasPermission>();
            var role = _railwayTicketDbContext.UserRoles.FirstOrDefault(x => x.RoleId == rolePermission.RoleId);
            if (role != null && rolePermission.RolePermissionInfo != null)
            {
                foreach (var item in rolePermission.RolePermissionInfo)
                {
                    var permissionData = _railwayTicketDbContext.RoleHasPermissions.Where(x => x.RoleId == role.RoleId && x.PermissionId == item.PermissionId).FirstOrDefault();
                    if (permissionData != null)
                    {
                        permissionData.IsChecked = item.IsChecked;
                        _railwayTicketDbContext.RoleHasPermissions.Update(permissionData);
                    } 
                }
                _railwayTicketDbContext.SaveChangesAsync();
            }
            return new ResponseModel(MessageModel.Success, true);
        }
    }
}
