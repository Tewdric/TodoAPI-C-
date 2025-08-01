// Controllers/AuthController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Dtos;
using TodoList.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Este e-mail já está em uso.");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuário registrado com sucesso!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRegisterDto request)
        {
            // CORREÇÃO: Trocado ":" por ";" no final da linha
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            // CORREÇÃO: "Unauthoried" para "Unauthorized"
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            // CORREÇÃO: "Enconding" para "Encoding" e "builder.Configuration" para "_configuration"
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // CORREÇÃO: "ClaimsIdentify" para "ClaimsIdentity"
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                // CORREÇÃO: "DataTime" para "DateTime"
                Expires = DateTime.UtcNow.AddHours(1),

                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],

                // CORREÇÃO: Sintaxe inteira do SigningCredentials
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}