using AuthService.Application.DTOs;
using AuthService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Application.Interface
{
    public interface IAuthService
    {
<<<<<<< HEAD
        Task<object> Login(LoginRequest request);
        Task<string> Register(RegisterRequest request);
        Task<User> GetUserByRefreshToken(string refreshToken);
        object GenerateNewToken(User user);
=======
        Task<string> Login(LoginRequest request);
        Task<string> Register(RegisterRequest request);
>>>>>>> 747e248aae7ae30a8fa30ea97bf166d4ddc85a78
    }
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetAll(CustomerQueryParams query);
        //Task<object> GetAll(CustomerQueryParams query);
        Task<CustomerResponse> GetById(int id);
        Task<string> Create(CustomerRequest request);
        Task<string> Update(int id, CustomerRequest request);
        Task<string> Delete(int id);

    }

    public interface ITeamService
    {
        Task<TeamResponse> Create(TeamRequest request);

        Task<List<TeamResponse>> GetAll();

        Task<TeamResponse> GetById(int id);

        Task<TeamResponse> Update(int id, TeamRequest request);

        Task<bool> Delete(int id);
    }

}

