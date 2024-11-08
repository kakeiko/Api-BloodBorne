
using API_Bloodborne.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Bloodborne.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[] 
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim("email", usuario.Email),
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tgur4532$%#SDOGUBNYUVFAS6fgs8yfxdbas908dfgh23IPB"));

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(10),
                claims:claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}