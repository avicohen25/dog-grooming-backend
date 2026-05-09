using Dapper;
using DogGrooming.Managers.Contracts;
using DogGrooming.Managers.Managers;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Models.Response;
using DogGrooming.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DogGrooming.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwt;
        private readonly ILoginManager _loginManager;

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration, JwtService jwt, ILoginManager loginManager)
        {
            _logger = logger;
            _configuration = configuration;
            _jwt = jwt;
            _loginManager = loginManager;
        }


        [HttpPost("Login")]
        public async Task<LoginResponse> Login(LoginParams loginParams)
        {
            LoginResponse response = await _loginManager.Login(loginParams);
            return response;
        }



        [HttpPost("Register")]
        public async Task<RegisterResponse> Register(RegisterParams registerParams)
        {
            RegisterResponse response = await _loginManager.Register(registerParams);
            return response;
        }


        [Authorize]
        [HttpGet("CustomerInit")]
        public async Task<LoginResponse> CustomerInit()
        {
            var userId = User.FindFirst("UserId")?.Value;

            LoginResponse response = await _loginManager.GetUserById(userId);
            return response;
        }


    }

}
