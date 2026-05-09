using DogGrooming.Models.Request;
using DogGrooming.Models.Response;

namespace DogGrooming.Managers.Contracts
{
    public interface IAppointmentsManager
    {
        Task<AppointmentsResponse> GetAppointments();
        Task<GetAppointmentDataResponse> GetAppointmentData(GetAppointmentDataParams getAppointmentDataParams);
        Task<AddAppointmentResponse> AddAppointment(AddAppointmentParams addAppointmentParams);
        Task<AddAppointmentResponse> UpdateAppointment(UpdateAppointmentParams updateAppointmentParams);
        Task<DeleteAppointmentResponse> DeleteAppointment(DeleteAppointmentParams deleteAppointmentParams);

    }
}
