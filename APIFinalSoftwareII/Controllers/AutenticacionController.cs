using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIFinalSoftwareII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacionController : Controller
    {
        private readonly IConfiguration configuration;
        public AutenticacionController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [Route("GetToken")]
        [HttpPost]
        public Models.Token GetToken([FromBody] Models.Usuario usuario)
        {
            var token = new Models.Token();
            var expiryTime = DateTime.Now.AddMinutes(24);
            var userName = usuario.Usuario1;
            token.token = CustomTokenJWT(userName, expiryTime);
            token.expiryTime = expiryTime;
            return token;
        }
        private string CustomTokenJWT(string ApplicationName, DateTime token_expiration)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(

                    Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])

                );

            var _signingCredentials = new SigningCredentials(

                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256

                );

            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(JwtRegisteredClaimNames.NameId, ApplicationName)

            };

            var _Payload = new JwtPayload(

                    issuer: configuration["JWT:Issuer"],

                    audience: configuration["JWT:Audience"],

                    claims: _Claims,

                    notBefore: DateTime.Now,

                    expires: token_expiration

                );

            var _Token = new JwtSecurityToken(

                    _Header,

                    _Payload

                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);

        }
    }
}
