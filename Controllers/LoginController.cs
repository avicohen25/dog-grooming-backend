using Dapper;
using DogGrooming.Models;
using DogGrooming.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

namespace DogGrooming.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwt;

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration, JwtService jwt)
        {
            _logger = logger;
            _configuration = configuration;
            _jwt = jwt;
        }


        [HttpPost("Login")]
        public async Task<LoginResponse> Login(LoginParams loginParams)
        {
            Thread.Sleep(1000); //TODO:delete this

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

            User user = result.FirstOrDefault();


            bool isValid = user != null;

            if (isValid)
            {
                var token = _jwt.GenerateToken(user.Email, user.Id.ToString());
                return new LoginResponse { isSuccess = true, token = token, user = user };
            }

            return new LoginResponse { isSuccess = false, token = "" };
        }



        [HttpPost("Register")]
        public async Task<RegisterResponse> Register(RegisterParams registerParams)
        {
            Thread.Sleep(1000); //TODO:delete this

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

            User user = result.FirstOrDefault();

            bool isValid = user != null;

            if (isValid)
            {
                var token = _jwt.GenerateToken(user.Email, user.Id.ToString());
                return new RegisterResponse { isSuccess = true, token = token, user = user };
            }

            return new RegisterResponse { isSuccess = false, token = "" };
        }


        [Authorize]
        [HttpGet("CustomerInit")]
        public async Task<LoginResponse> CustomerInit()
        {
            Thread.Sleep(3000); //TODO:delete this

            var userId = User.FindFirst("UserId")?.Value;

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

            User user = result.FirstOrDefault();

            bool isValid = user != null;

            return new LoginResponse { isSuccess = isValid, token = "", user = user };

        }


    }


    public class LoginParams
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginResponse
    {
        public bool isSuccess { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }


    public class RegisterParams
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class RegisterResponse
    {
        public bool isSuccess { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }
}
