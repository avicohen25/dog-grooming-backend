using Dapper;
using DogGrooming.Models;
using DogGrooming.Models.Response;
using DogGrooming.Providers.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DogGrooming.Providers.Providers
{
    public class GetHaircutTypesProvider : IGetHaircutTypesProvider
    {
        private readonly IConfiguration _configuration;

        public GetHaircutTypesProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<HaircutType>> GetData()
        {
            List<HaircutType> haircutTypes = new List<HaircutType>();

            try
            {
                string connectionString = _configuration["ConnectionStrings:DogGrooming"];
                using var conn = new SqlConnection(connectionString);

                var result = await conn.QueryAsync<HaircutType>(
                    "sp_getHaircutTypes_select",
                    commandType: CommandType.StoredProcedure
                );

                haircutTypes = result.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

            return haircutTypes;

        }
    }
}
