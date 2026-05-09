using Dapper;
using DogGrooming.Models;
using DogGrooming.Providers.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DogGrooming.Providers.Providers
{
    public class GetUserByIdProvider : IGetUserByIdProvider
    {
        private readonly IConfiguration _configuration;

        public GetUserByIdProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> GetData(string userId)
        {
            User user = null;

            try
            {
                string connectionString = _configuration["ConnectionStrings:DogGrooming"];
                using var conn = new SqlConnection(connectionString);

                var result = await conn.QueryAsync<User>(
                    "sp_getUserById_select",
                    new
                    {
                        UserId = userId
                    },
                    commandType: CommandType.StoredProcedure
                );

                user = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }

            return user;

        }
    }
}
