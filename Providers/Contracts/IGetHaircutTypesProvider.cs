using DogGrooming.Models.Response;

namespace DogGrooming.Providers.Contracts
{
    public interface IGetHaircutTypesProvider
    {
        Task<List<HaircutType>> GetData();
    }
}
