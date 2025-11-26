using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.JwtTokenDTO
{
    public class JwtTokenResponse
    {
        public string? AccessToken { get; set; }
        public int ExpireIn { get; set; }
    }

    public class RefreshTokenDTO
    {
        public string Token { get; set; } = null!;

        public DateTime Expire { get; set; }

        public int UserId { get; set; }
    }

    public class RefreshRequestDTO
    {
        public string? Token {  set; get; }
    }
}
