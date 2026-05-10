using DogGrooming.Models;
using DogGrooming.Models.Request;

namespace DogGrooming.Providers.Contracts
{
    public interface IGetAppointmentsProvider
    {
        Task<List<Appointment>> GetData(GetAppointmentsParams getAppointmentsParams);
    }
}
