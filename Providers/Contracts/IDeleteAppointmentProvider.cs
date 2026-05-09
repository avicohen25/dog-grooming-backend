using DogGrooming.Models;
using DogGrooming.Models.Request;

namespace DogGrooming.Providers.Contracts
{
    public interface IDeleteAppointmentProvider
    {
        Task<bool> GetData(DeleteAppointmentParams deleteAppointmentParams);
    }
}
