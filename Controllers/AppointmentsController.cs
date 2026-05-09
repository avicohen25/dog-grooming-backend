using DogGrooming.Managers.Contracts;
using DogGrooming.Models.Request;
using DogGrooming.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogGrooming.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAppointmentsManager _appointmentsManager;

        public AppointmentsController(ILogger<AppointmentsController> logger, IConfiguration configuration, IAppointmentsManager appointmentsManager)
        {
            _logger = logger;
            _configuration = configuration;
            _appointmentsManager = appointmentsManager;
        }


        [Authorize]
        [HttpGet("GetAppointments")]
        public async Task<AppointmentsResponse> GetAppointments()
        {
            AppointmentsResponse response = await _appointmentsManager.GetAppointments();
            return response;
        }


        [Authorize]
        [HttpPost("GetAppointmentData")]
        public async Task<GetAppointmentDataResponse> GetAppointmentData(GetAppointmentDataParams getAppointmentDataParams)
        {
            GetAppointmentDataResponse response = await _appointmentsManager.GetAppointmentData(getAppointmentDataParams);
            return response;
        }

        [Authorize]
        [HttpPost("AddAppointment")]
        public async Task<AddAppointmentResponse> AddAppointment(AddAppointmentParams addAppointmentParams)
        {
            var userId = User.FindFirst("UserId")?.Value;
            addAppointmentParams.userId = int.Parse(userId);

            AddAppointmentResponse response = await _appointmentsManager.AddAppointment(addAppointmentParams);
            return response;
        }


        [Authorize]
        [HttpPost("UpdateAppointment")]
        public async Task<AddAppointmentResponse> UpdateAppointment(UpdateAppointmentParams updateAppointmentParams)
        {
            var userId = User.FindFirst("UserId")?.Value;
            updateAppointmentParams.userId = int.Parse(userId);

            AddAppointmentResponse response = await _appointmentsManager.UpdateAppointment(updateAppointmentParams);
            return response;
        }


        [Authorize]
        [HttpPost("DeleteAppointment")]
        public async Task<DeleteAppointmentResponse> DeleteAppointment(DeleteAppointmentParams deleteAppointmentParams)
        {
            var userId = User.FindFirst("UserId")?.Value;
            deleteAppointmentParams.userId = int.Parse(userId);

            DeleteAppointmentResponse response = await _appointmentsManager.DeleteAppointment(deleteAppointmentParams);
            return response;
        }


    }

}
