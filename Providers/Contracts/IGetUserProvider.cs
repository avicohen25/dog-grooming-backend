using DogGrooming.Models;
using DogGrooming.Models.Request;

namespace DogGrooming.Providers.Contracts
{
    public interface IGetUserProvider
    {
        Task<User> GetData(LoginParams loginParams);
    }
}
