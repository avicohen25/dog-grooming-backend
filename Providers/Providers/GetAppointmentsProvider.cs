using Dapper;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Providers.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DogGrooming.Providers.Providers
{
    public class GetAppointmentsProvider: IGetAppointmentsProvider
    {
        private readonly IConfiguration _configuration;

        public GetAppointmentsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Appointment>> GetData(GetAppointmentsParams getAppointmentsParams)
        {
            List<Appointment> appointments = new List<Appointment>();

            try
            {
                string connectionString = _configuration["ConnectionStrings:DogGrooming"];
                using var conn = new SqlConnection(connectionString);

                var result = await conn.QueryAsync<Appointment>(
                    "sp_getAppointments_select",
                    new
                    {
                        StartDate = getAppointmentsParams.startDate,
                        EndDate = getAppointmentsParams.endDate,
                        UserName = getAppointmentsParams.userName
                    },
                    commandType: CommandType.StoredProcedure
                );

                appointments = result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

            return appointments;

        }
    }
}
