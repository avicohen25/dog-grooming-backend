using DogGrooming.Models;

namespace DogGrooming.Providers.Contracts
{
    public interface IGetAppointmentsProvider
    {
        Task<List<Appointment>> GetData();
    }
}
