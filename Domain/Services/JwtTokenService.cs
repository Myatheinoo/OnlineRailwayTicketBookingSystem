using AutoMapper;
using Azure.Core;
using Database.DTO;
using Database.DTO.JwtTokenDTO;
using Database.DTO.Shared;
using Database.DTO.UserDTO;
using Database.Models;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly RailwayTicketDbContext _context;
        public JwtTokenService(IConfiguration configuration,RailwayTicketDbContext railwayTicketDbContext,IMapper mapper)
        {
            _configuration = configuration;
            _context = railwayTicketDbContext;
            _mapper = mapper; 
        }

        public JwtTokenResponse GenerateToken(string email, string roleName)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required to generate token");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, email),
               new Claim(ClaimTypes.Role, roleName),
               //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               //new Claim(ClaimTypes.Role, getUserType(user.RoleId.ToString()))
            };
            var tokenValidityMins = _configuration.GetValue<int>("Jwt:TokenValidityMins");
            var tokenExpireTimeStapm = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: tokenExpireTimeStapm,
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtTokenResponse
            {
                AccessToken = accessToken,
                ExpireIn = (int)(tokenExpireTimeStapm - DateTime.UtcNow).TotalSeconds
                //ExpireIn = (int)tokenExpireTimeStapm.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }

        public string GenerateRefreshToken(int userId)
        {
            var refreshTokenValidityMins = _configuration.GetValue<int>("Jwt:RefreshTokenValidityMins");
            var refreshToken = new RefreshTokenDTO
            {
                Token = Guid.NewGuid().ToString(),
                Expire = DateTime.UtcNow.AddMinutes(refreshTokenValidityMins),
                UserId = userId
            };

            var token = CreateRefreshToken(refreshToken);

            return refreshToken.Token;
        }

        public async Task<JwtTokenResponse?> ValidateRefreshToken(string token)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
            if (refreshToken is null || refreshToken.Expire < DateTime.UtcNow)
            {
                return null;  
            }
            await DeleteToken(refreshToken.Token);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == refreshToken.UserId);
            var role = await _context.UserRoles.FirstOrDefaultAsync(x => x.RoleId == user!.RoleId);

            if (user is null || role is null) return null;

            return GenerateToken(user.Email, role.Name);
        }

        public ResponseModel CreateRefreshToken(RefreshTokenDTO refreshToken)
        {
            var token = _mapper.Map<RefreshToken>(refreshToken);

             _context.RefreshTokens.Add(token);
             _context.SaveChanges();

            return new ResponseModel(MessageModel.AddSuccess,true,token);
        }

        public async Task<ResponseModel> DeleteToken(string token)
        {
            var usertoken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
            if(usertoken is not null)
            {
                _context.RefreshTokens.Remove(usertoken);
                await _context.SaveChangesAsync();

                return new ResponseModel(MessageModel.DeleteSuccess,true);
            }
            return new ResponseModel(MessageModel.UnSuccess, false);
        }
    }
}
