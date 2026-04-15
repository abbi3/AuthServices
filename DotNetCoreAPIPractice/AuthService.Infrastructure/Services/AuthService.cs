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
using AuthService.Domain;
//using AuthService.Domain.Entities;

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
        public async Task<object> Login(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == request.Username.ToLower());

            if (user == null)
                throw new Exception("User not found");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid password");

            // 🔥 Generate JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // 🔥 Generate Refresh Token
            var refreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _context.SaveChangesAsync();
            return new
            {
                accessToken,
                refreshToken
            };
        }
        public async Task<string> Register(RegisterRequest request)
        {
            // Check if user exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == request.Username);

            if (existingUser != null)
                throw new Exception("User already exists");

            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create user
            var user = new User
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                role = string.IsNullOrEmpty(request.role) ? "User" : request.role
            };

            // Save to DB

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully";
        }
        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }

        public object GenerateNewToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.role)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            return new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

    }
}    

