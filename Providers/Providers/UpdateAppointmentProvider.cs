using Dapper;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Providers.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DogGrooming.Providers.Providers
{
    public class UpdateAppointmentProvider : IUpdateAppointmentProvider
    {
        private readonly IConfiguration _configuration;

        public UpdateAppointmentProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> GetData(UpdateAppointmentParams updateAppointmentParams)
        {
            try
            {
                TimeSpan time = TimeSpan.Parse(updateAppointmentParams.selectedSlot);
                DateTime finalDateTime = updateAppointmentParams.haircutDate.Date + time;

                string connectionString = _configuration["ConnectionStrings:DogGrooming"];
                using var conn = new SqlConnection(connectionString);

                var result = await conn.QueryAsync<Appointment>(
                    "sp_updateAppointment_update",
                    new
                    {
                        AppointmentId = updateAppointmentParams.appointmentId,
                        UserId = updateAppointmentParams.userId,
                        HaircutTypeId = updateAppointmentParams.haircutTypeId,
                        HaircutDate = finalDateTime,
                    },
                    commandType: CommandType.StoredProcedure
                );

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
