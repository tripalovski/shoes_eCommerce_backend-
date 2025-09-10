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
using System.Security.Cryptography;

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
        public async Task<ActionResult<TokenResponseDto>> Register(RegisterUserDto userDto)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == userDto.Email);
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
            await _context.SaveChangesAsync();

            var token = CreateToken(newUser);
            return new TokenResponseDto { AccessToken = token };
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginUserDto loginUserDto) {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);
            if (user == null) {
                return Unauthorized("Invalid email or password");
            }
            if (!BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash)) {
                return Unauthorized("Invalid email or password");
            }
            var token = CreateToken(user);
            return new TokenResponseDto { AccessToken = token };
        }

        [Authorize(Roles = nameof(Role.Admin))]
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

        private string CreateToken(User user) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
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
    }
}
