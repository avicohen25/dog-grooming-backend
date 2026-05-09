using Dapper;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Providers.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DogGrooming.Providers.Providers
{
    public class AddAppointmentProvider : IAddAppointmentProvider
    {
        private readonly IConfiguration _configuration;

        public AddAppointmentProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> GetData(AddAppointmentParams addAppointmentParams)
        {
            try
            {
                TimeSpan time = TimeSpan.Parse(addAppointmentParams.selectedSlot);
                DateTime finalDateTime = addAppointmentParams.haircutDate.Date + time;

                string connectionString = _configuration["ConnectionStrings:DogGrooming"];
                using var conn = new SqlConnection(connectionString);

                var result = await conn.QueryAsync<Appointment>(
                    "sp_insertAppointment_insert",
                    new
                    {
                        UserId = addAppointmentParams.userId,
                        HaircutTypeId = addAppointmentParams.haircutTypeId,
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
