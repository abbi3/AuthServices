using AuthService.Application.DTOs;
using System.Threading.Tasks;

namespace AuthService.Application.Interface
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequest request);
        Task<string> Register(RegisterRequest request);
    }
}

