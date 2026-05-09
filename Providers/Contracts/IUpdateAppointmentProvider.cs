using DogGrooming.Models;
using DogGrooming.Models.Request;

namespace DogGrooming.Providers.Contracts
{
    public interface IUpdateAppointmentProvider
    {
        Task<bool> GetData(UpdateAppointmentParams updateAppointmentParams);
    }
}
