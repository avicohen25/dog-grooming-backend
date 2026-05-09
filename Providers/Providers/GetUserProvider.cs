using Dapper;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Providers.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DogGrooming.Providers.Providers
{
    public class GetUserProvider : IGetUserProvider
    {
        private readonly IConfiguration _configuration;

        public GetUserProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> GetData(LoginParams loginParams)
        {
            User user = null;

            try
            {
                string connectionString = _configuration["ConnectionStrings:DogGrooming"];
                using var conn = new SqlConnection(connectionString);

                var result = await conn.QueryAsync<User>(
                    "sp_getUser_select",
                    new
                    {
                        Email = loginParams.email,
                        PasswordHash = loginParams.password
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
