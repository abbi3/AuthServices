using AuthService.Application.DTOs;
using AuthService.Application.Interface;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthDBContext _context;
        private readonly IConfiguration _config;
        public AuthService(AuthDBContext context , IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<string> Login(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null)
                throw new Exception("User not found");

            if (user.PasswordHash != request.Password)
                throw new Exception("Invalid password");

            // Token logic will come next step
            var claims = new[]
            {
                 new Claim(ClaimTypes.Name, user.Username)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken( issuer: _config["Jwt:Issuer"],audience: _config["Jwt:Audience"],claims: claims,expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"])),signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
}