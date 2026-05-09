using DogGrooming.Models.Request;
using DogGrooming.Models.Response;

namespace DogGrooming.Managers.Contracts
{
    public interface ILoginManager
    {
        Task<LoginResponse> Login(LoginParams loginParams);
        Task<RegisterResponse> Register(RegisterParams registerParams);
        Task<LoginResponse> GetUserById(string userId);
    }
}
