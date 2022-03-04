using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheets
{
    public class TokenResponce
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public string GenerateToken(string login, string key, DateTimeOffset dateTimeOffset)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login)
                }),
                Expires = DateTime.Now.AddTicks(dateTimeOffset.Ticks),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}
