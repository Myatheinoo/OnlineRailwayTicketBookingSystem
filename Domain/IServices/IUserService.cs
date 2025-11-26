using Database.DTO;
using Database.DTO.UserDTO;
using Database.Models;

namespace Domain.IServices
{
    public interface IUserService
    {
        public Task<ResponseModel> AddUser(UserRequestModel userModel);
        public ResponseModel GetAllUser();
        public ResponseModel GetByEachUser(int id);
        public ResponseModel UpdateUser(UpdateUserRequestModel requestModel);
        public ResponseModel DeleteUser(int id);
        public Task<LoginResponseModel> UserLogin(LoginRequestModel loginModel);
        //public string GenerateToken(User user,string roleName);
        public  (string PasswordHash, string PasswordSalt) HashPassword(string plainPassword);
        public bool VerifyPassword(string plainPassword, string storedHash, string storedSalt);
    }
}