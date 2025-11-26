using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.UserDTO
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
    public class UserRequestModel
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
        public int RoleID { get; set; }

        public int CreatedBy { get; set; }
        public byte ActiveFlag { get; set; }
    }
    public class UpdateUserRequestModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class LoginRequestModel
    {
        public string? Email { get; set; }
        public string? password { get; set; }
    }
    public class LoginResponseModel
    {
        public LoginResponseModel(bool isSuccess, string? message, object? data = null, string? refreshToken = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            RefreshToken = refreshToken;
        }
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
        public string? RefreshToken { get; set; }
    }
}
