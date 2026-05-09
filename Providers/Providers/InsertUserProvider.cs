using Dapper;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Providers.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DogGrooming.Providers.Providers
{
    public class InsertUserProvider : IInsertUserProvider
    {
        private readonly IConfiguration _configuration;

        public InsertUserProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> GetData(RegisterParams registerParams)
        {
            User user = null;

            try
            {
                string connectionString = _configuration["ConnectionStrings:DogGrooming"];
                using var conn = new SqlConnection(connectionString);

                var result = await conn.QueryAsync<User>(
                    "sp_insertUser_insert",
                    new
                    {
                        FirstName = registerParams.firstName,
                        LastName = registerParams.lastName,
                        Email = registerParams.email,
                        PasswordHash = registerParams.password,
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
