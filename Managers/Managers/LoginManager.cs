using DogGrooming.Managers.Contracts;
using DogGrooming.Models;
using DogGrooming.Models.Request;
using DogGrooming.Models.Response;
using DogGrooming.Providers.Contracts;
using DogGrooming.Providers.Providers;
using DogGrooming.Utils;
using System.Globalization;

namespace DogGrooming.Managers.Managers
{
    public class LoginManager : ILoginManager
    {
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwt;
        private readonly IGetUserProvider _getUserProvider;
        private readonly IInsertUserProvider _insertUserProvider;
        private readonly IGetUserByIdProvider _getUserByIdProvider;

        public LoginManager(IConfiguration configuration, JwtService jwt, IGetUserProvider getUserProvider, IInsertUserProvider insertUserProvider, IGetUserByIdProvider getUserByIdProvider)
        {
            _configuration = configuration;
            _jwt = jwt;
            _getUserProvider = getUserProvider;
            _insertUserProvider = insertUserProvider;
            _getUserByIdProvider = getUserByIdProvider;
        }


        public async Task<LoginResponse> Login(LoginParams loginParams)
        {
            User user = await _getUserProvider.GetData(loginParams);

            bool isValid = user != null;

            if (isValid)
            {
                var token = _jwt.GenerateToken(user.Email, user.Id.ToString());
                return new LoginResponse { isSuccess = true, token = token, user = user };
            }

            return new LoginResponse { isSuccess = false, token = "" };
        }


        public async Task<RegisterResponse> Register(RegisterParams registerParams)
        {
            User user = await _insertUserProvider.GetData(registerParams);

            bool isValid = user != null;

            if (isValid)
            {
                var token = _jwt.GenerateToken(user.Email, user.Id.ToString());
                return new RegisterResponse { isSuccess = true, token = token, user = user };
            }

            return new RegisterResponse { isSuccess = false, token = "" };
        }


        public async Task<LoginResponse> GetUserById(string userId)
        {
            User user = await _getUserByIdProvider.GetData(userId);

            bool isValid = user != null;

            return new LoginResponse { isSuccess = isValid, token = "", user = user };
        }

    }
}
