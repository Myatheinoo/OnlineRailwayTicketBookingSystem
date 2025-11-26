using Database.DTO;
using Database.DTO.Shared;
using Database.DTO.UserDTO;
using Database.Models;
using Domain.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly RailwayTicketDbContext _railwayTicketDbContext;
        private readonly IConfiguration _config;
        private readonly IJwtTokenService _jwtTokenService;
        public UserService(RailwayTicketDbContext railwayTicketDbContext,IConfiguration config, IJwtTokenService jwtTokenService)
        {
            _config = config;
            _railwayTicketDbContext = railwayTicketDbContext;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ResponseModel> AddUser(UserRequestModel userModel)
        {
            try
            {
                var userlist = await _railwayTicketDbContext.Users.Where(x => x.Email == userModel.Email).ToListAsync();
                var roleList = await _railwayTicketDbContext.UserRoles.Where(x => x.RoleId == userModel.RoleID).FirstOrDefaultAsync();
                var (passwordHash, passwordSalt) = HashPassword(userModel.Password!);

                if (userlist.Count == 0 && roleList != null && passwordHash != null && passwordSalt !=null)
                {
                    var user = new User
                    {
                        UserName = userModel.UserName!,
                        Email = userModel.Email!,
                        PasswordHash = passwordHash,
                        SaltKey = passwordSalt,
                        RoleId = userModel.RoleID,
                        UpdatedBy = 1,
                        CreatedBy = 1,
                        CreatedDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        ActiveFlag = userModel.ActiveFlag,
                    };
                    await _railwayTicketDbContext.Users.AddAsync(user);
                    await _railwayTicketDbContext.SaveChangesAsync();
                    return new ResponseModel(MessageModel.AddSuccess, true);
                   
                }
                else
                {
                    return new ResponseModel(MessageModel.UnSuccess, false);
                }
                
            }
            catch (Exception ex)
            {
                return new ResponseModel(ex.ToString(), false);
            }
        }

        public ResponseModel GetAllUser()
        {
            try
            {
                //var list = _railwayTicketDbContext.Users.Where(x =>x.ActiveFlag == 0).ToList();
                var userList = (from u in _railwayTicketDbContext.Users
                                join ur in _railwayTicketDbContext.UserRoles on u.RoleId equals ur.RoleId
                                where u.ActiveFlag == 0
                                select new UserModel
                                {
                                    UserName = u.UserName,
                                    Email = u.Email,
                                    RoleName = ur.Name
                                }).ToList();

                return new ResponseModel(MessageModel.Success,true, userList);
            }
            catch (Exception ex)
            {
                return new ResponseModel(ex.Message,false);
            }
        }
        public ResponseModel GetByEachUser(int id)
        {
            try
            {
                if(id == 0)
                {
                    return new ResponseModel("Enter valid user id.", false);
                }
                var user = _railwayTicketDbContext.Users.FirstOrDefault(x => x.Id == id && x.ActiveFlag == 0);
                var role = _railwayTicketDbContext.UserRoles.FirstOrDefault(x => x.RoleId == user!.RoleId);

                if (user is null)
                {
                    return new ResponseModel("Use not found.", false);
                }
                var user_data = new UserModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleName = role!.Name,
                };
                return new ResponseModel(MessageModel.Success, true, user_data);
            }
            catch (Exception ex)
            {
                return new ResponseModel(ex.Message,false);
            }
        }
        public ResponseModel UpdateUser(UpdateUserRequestModel requestModel)
        {
            try
            {
                var user = _railwayTicketDbContext.Users.Where(x => x.Id == requestModel.Id).FirstOrDefault();
                var role = _railwayTicketDbContext.UserRoles.Where(x => x.RoleId == requestModel.RoleId).FirstOrDefault();

                if (string.IsNullOrEmpty(requestModel.UserName) || string.IsNullOrEmpty(requestModel.Email) || string.IsNullOrEmpty(requestModel.Password) || requestModel.RoleId == 0)
                {
                    return new ResponseModel("Fill all fields.Enter valid fileds.", false);
                }

                if (user is null || role is null)
                {
                    return new ResponseModel("User not found.", false);
                }
                var (passwordHash, passwordSalt) = HashPassword(requestModel.Password);

                user.UserName = requestModel.UserName!;
                user.Email = requestModel.Email!;
                user.PasswordHash = passwordHash!;
                user.SaltKey = passwordSalt!;
                user.RoleId = requestModel.RoleId;
                user.UpdatedBy = requestModel.UpdatedBy;
                user.UpdateDate = DateTime.UtcNow;

                _railwayTicketDbContext.SaveChanges();
                return new ResponseModel(MessageModel.UpdateSuccess, true, requestModel);
            }
            catch(Exception ex)
            {
                return new ResponseModel(ex.Message,false);
            }
        }
        public ResponseModel DeleteUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new ResponseModel("Enter valid user id.", false);
                }

                var user = _railwayTicketDbContext.Users.Where(x => x.Id == id && x.ActiveFlag == 0).FirstOrDefault();
                if (user is null)
                {
                    return new ResponseModel("User not found.", false);
                }

                user.ActiveFlag = 1;
                _railwayTicketDbContext.SaveChanges();
                return new ResponseModel(MessageModel.DeleteSuccess, true);
            }
            catch(Exception ex)
            {
                return new ResponseModel(ex.Message, false);
            }
        }

        public async Task<LoginResponseModel> UserLogin(LoginRequestModel loginRequest)
        {
            //await using var trasaction = await _railwayTicketDbContext.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.password))
                    return new LoginResponseModel(false, "Fill all fields.Enter valid fileds.");

                var user = await _railwayTicketDbContext.Users.FirstOrDefaultAsync(x=> x.Email == loginRequest.Email && x.ActiveFlag == 0);

                if (user is not null)
                {
                    var userRole = await _railwayTicketDbContext.UserRoles.FirstOrDefaultAsync(x => x.RoleId == user.RoleId);
                    var password = VerifyPassword(loginRequest.password, user.PasswordHash!, user.SaltKey!);
                    if (userRole is not null && password)
                    {
                        var token =  _jwtTokenService.GenerateToken(user.Email, userRole.Name);
                        var refreshToken = _jwtTokenService.GenerateRefreshToken(user.Id);

                        user.Token = refreshToken;
                        _railwayTicketDbContext.Users.Update(user);
                       await _railwayTicketDbContext.SaveAndDetachAsync();

                        //await trasaction.CommitAsync();

                        return new LoginResponseModel(true, "Login successful", token,refreshToken); 
                    }
                    return new LoginResponseModel(false, "Wrong Password.");
                }
                return new LoginResponseModel(false, "User not found");
            }
            catch (Exception ex)
            {
                //await trasaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        //public LoginResponseModel UserLogin(LoginRequestModel loginModel)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.password))
        //        {
        //            return new LoginResponseModel(false,"Fill all fields.Enter valid fileds.");
        //        }
        //        var user = _railwayTicketDbContext.Users.FirstOrDefault(x => x.Email == loginModel.Email && x.ActiveFlag == 0);
        //        if (user is not null)
        //        {
        //            var roleData = _railwayTicketDbContext.UserRoles.FirstOrDefault(x => x.RoleId == user.RoleId);

        //            var userPassword = VerifyPassword(loginModel.password, user.PasswordHash!, user.SaltKey!);
        //            if (userPassword && roleData is not null)
        //            {
        //                var token = GenerateToken(user,roleData.Name);
        //                return new LoginResponseModel(true, MessageModel.Success, token);
        //            }
        //            return new LoginResponseModel(false, "Wrong Password.");
        //        }
        //        else
        //        {
        //            return new LoginResponseModel(false, "User not found");
        //        }

        //    }catch(Exception ex)
        //    {
        //        return new LoginResponseModel(false, ex.Message);
        //    }
        //}
        //public string GenerateToken(User user,string roleName)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //       new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //       new Claim(ClaimTypes.Role, roleName),
        //       //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //       //new Claim(ClaimTypes.Role, getUserType(user.RoleId.ToString()))
        //    };

        //    var token = new JwtSecurityToken(
        //        issuer: _config["Jwt:Issuer"],
        //        audience: _config["Jwt:Audience"],
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(30),
        //        signingCredentials: credentials
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        // Generate hash and salt
        public (string PasswordHash, string PasswordSalt) HashPassword(string plainPassword)
        {
            // Generate salt (BCrypt salt includes cost factor)
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hash = BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);

            return (hash, salt);
        }

        // Verify password using hash and salt
        public bool VerifyPassword(string plainPassword, string storedHash, string storedSalt)
        {
            // Re-hash the plain password using the stored salt
            string hashToCheck = BCrypt.Net.BCrypt.HashPassword(plainPassword, storedSalt);
            return hashToCheck == storedHash;
        }
    }
}
