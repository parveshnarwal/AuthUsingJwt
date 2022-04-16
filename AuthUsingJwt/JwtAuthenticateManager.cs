using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthUsingJwt
{
    public class JwtAuthenticateManager : IJwtAuthenticateManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            {"test1", "password1" },
            {"test2", "password2" }
        };

        private readonly string key;

        public JwtAuthenticateManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string username, string password)
        {
            if(!users.Any(x => x.Key == username && x.Value == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
