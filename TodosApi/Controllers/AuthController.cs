/* using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TodosApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
public static class AuthConfig
{
    public static string SigningKey { get; } = "your-secret-key"; // Replace with your actual secret key
}
        private readonly string _secretKey = AuthConfig.SigningKey;
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, username),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

    }
}
 */