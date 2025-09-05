using eCommerce_backend.Constants;
using eCommerce_backend.Database;
using eCommerce_backend.DTOs;
using eCommerce_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eCommerce_backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;
        public UserController(ApplicationDbContext context, IConfiguration configuration) {
            _context = context;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserDto userDto)
        {
            var existingUser = _context.User.FirstOrDefault(u => u.Email == userDto.Email);
            if (existingUser != null)
            {
                return BadRequest("User with this email already exists");
            }

            var newUser = new User
            {
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };

            _context.Add(newUser);
            _context.SaveChanges();
            return Ok(new { userId = newUser.Id });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto loginUserDto)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == loginUserDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }
            if(!BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        [Authorize]
        [HttpPost("getAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers() {
            return await _context.User
                .Select(u => new UserDto {
                    Email = u.Email,
                    FirstName = u.LastName,
                    LastName = u.LastName,
                    CreatedAt = u.CreatedAt
                }).ToListAsync();
        }
    }
}
