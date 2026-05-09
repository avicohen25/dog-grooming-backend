using DogGrooming.Models;
using DogGrooming.Models.Request;

namespace DogGrooming.Providers.Contracts
{
    public interface IAddAppointmentProvider
    {
        Task<bool> GetData(AddAppointmentParams addAppointmentParams);
    }
}
