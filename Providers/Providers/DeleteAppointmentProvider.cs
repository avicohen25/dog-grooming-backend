using Dapper;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Providers.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DogGrooming.Providers.Providers
{
    public class DeleteAppointmentProvider : IDeleteAppointmentProvider
    {
        private readonly IConfiguration _configuration;

        public DeleteAppointmentProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> GetData(DeleteAppointmentParams deleteAppointmentParams)
        {
            try
            {
                string connectionString = _configuration["ConnectionStrings:DogGrooming"];
                using var conn = new SqlConnection(connectionString);

                var result = await conn.QueryAsync<Appointment>(
                    "sp_deleteAppointment_delete",
                    new
                    {
                        AppointmentId = deleteAppointmentParams.AppointmentId,
                        UserId = deleteAppointmentParams.userId
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
