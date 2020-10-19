using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));

        public IConfiguration _config;
        private WarehouseManagementContext _context;

        public TokenController(IConfiguration config, WarehouseManagementContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        [Route("api/Token/LoginDetail")]
        public IActionResult LoginResult([FromBody] UserInfo user)
        {
            _log4net.Info("TokenController HttpPost Login Initiated ");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var existingUser = _context.UserInfo.Where(u => u.Username == user.Username).FirstOrDefault();
            if(existingUser == null)
            {
                _log4net.Info("TokenController Login Not Found ");
                return NotFound();
            }
            if(existingUser.Username == user.Username && existingUser.Password == user.Password)
            {
                _log4net.Info("TokenController Correct Credentials ");
                return Ok(new { token = GenerateJSONWebToken(existingUser) });
            }
            return BadRequest();
        }

        string GenerateJSONWebToken(UserInfo user)
        {
            _log4net.Info("TokenController Generating JSON Token ");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                _config["Jwt: Issuer"],
                _config["Jwt: Issuer"], null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
     
        }
    }
}
