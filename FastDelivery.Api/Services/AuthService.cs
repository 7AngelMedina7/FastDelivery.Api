using BCrypt.Net;
using FastDelivery.Api.Data;
using FastDelivery.Api.DTOs.Auth;
using FastDelivery.Api.DTOs.User;
using FastDelivery.Api.Models;
using FastDelivery.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FastDelivery.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            //Validar si el email esta registrado previamente
            var emailExist = await _context.Users.AnyAsync(
                    x => x.Email == dto.Email
                );
            if (emailExist)
            {
                throw new Exception("Correo Previamente Registrado");
            }
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password, 10),
                Role = dto.Role,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Name = user.Name,
                Email = user.Email,
                Message = "Usuario creado correctamente"
            };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                x => x.Email == dto.Email
            );
            if (user == null)
            {
                throw new Exception("Información del Usuario Invalida");
            }
            var validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!validPassword)
            {
                throw new Exception("Información del Usuario Invalida");
            }
            var token = await GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Name = user.Name,
                Email = user.Email,
                Token = token,
                Message = "Inicio de Sesión Correcto"
            };
        }
        public async Task<UserDto> ProfileAsync(Guid id)
        {
            var user = await _context.Users
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

            if (user == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
            };
        }
        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("Role", user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
