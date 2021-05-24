using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ShopBridgeBC.Data;

namespace ShopBridgeBC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        private IAuthRepo _repo;

        private IConfiguration _config;
        public AuthController(ILogger<AuthController> logger, IAuthRepo repo, IConfiguration config)
        {
            _logger = logger;
            _repo = repo;
            _config = config;
        }
        //list items from inventory
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserRequest user)
        {
            if (_repo.UserExist(user).Result)
            {
                var newUser = await _repo.Login(user);
                if (newUser != null)
                {
                    var tokenString = GenerateJSONWebToken(newUser);
                    return Ok(tokenString);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            else
            {
                return StatusCode(400);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRequest user)
        {
            if (!_repo.UserExist(user).Result)
            {

                await _repo.Register(user);

                return StatusCode(200);
            }
            else
                return StatusCode(400);
        }

        private string GenerateJSONWebToken(UserResponse userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
