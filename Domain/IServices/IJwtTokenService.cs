using Database.DTO;
using Database.DTO.JwtTokenDTO;
using Database.DTO.UserDTO;
using Database.Models;

namespace Domain.IServices
{
    public interface IJwtTokenService
    {
        public Task<JwtTokenResponse?> ValidateRefreshToken(string token);
        public JwtTokenResponse GenerateToken(string email, string roleName);
        public string GenerateRefreshToken(int userId);
        public ResponseModel CreateRefreshToken(RefreshTokenDTO refreshToken);
    }
}
