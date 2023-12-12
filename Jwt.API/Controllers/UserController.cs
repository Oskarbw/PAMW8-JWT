using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jwt.API.Model;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using System;

namespace Jwt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<User> users = new List<User>(){
            new User
            {
                Username = "andrzej",
                Password = "abc123",
                IsAdmin = false
            },
            new User
            {
                Username = "admin",
                Password = "admin123",
                IsAdmin = true
            }
        };

        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuartion)
        {
            _configuration = configuartion;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var newUser = new User
            {
                Username = request.Username,
                Password = request.Password,
                IsAdmin = false
            };
            
            users.Add(newUser);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            var user = users.Find(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return BadRequest();
            }
            var token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var role = user.IsAdmin ? "Admin" : "User";

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                audience: _configuration.GetSection("JwtSettings:Audience").Value!,
                issuer: _configuration.GetSection("JwtSettings:Issuer").Value!,
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
