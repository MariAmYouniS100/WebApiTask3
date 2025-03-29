using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.GenerateToken
{
    public class TokenService
    {
        private static string SecretKey = "123456789012345612345678@#$%^SDFDEKJSKDJSKJOLPNMDMDMSHHTRSZPOQA"; 
        private static  string Issuer = "https://localhost:7155";
        private static  int ExpireMinutes = 60;
        public static string GenerateToken(string userId,string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                 issuer: Issuer,
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(ExpireMinutes),
                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
