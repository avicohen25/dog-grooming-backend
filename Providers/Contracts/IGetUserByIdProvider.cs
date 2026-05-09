using DogGrooming.Models;

namespace DogGrooming.Providers.Contracts
{
    public interface IGetUserByIdProvider
    {
        Task<User> GetData(string userId);
    }
}
