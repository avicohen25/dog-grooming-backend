using DogGrooming.Models;
using DogGrooming.Models.Request;

namespace DogGrooming.Providers.Contracts
{
    public interface IInsertUserProvider
    {
        Task<User> GetData(RegisterParams registerParams);
    }
}
