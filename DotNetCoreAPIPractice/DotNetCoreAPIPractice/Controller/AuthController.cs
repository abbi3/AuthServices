using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuthService.Application.DTOs;
using AuthService.Application.Interface;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.Login(request);
            return Ok(new { Token = token });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request);
            return Ok(new { Message = result });
        }

        //Admin
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult GetAdminData()
        {
            return Ok("This is Admin data");
        }

        //User
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Ok("User profile");
        }
    }
}
