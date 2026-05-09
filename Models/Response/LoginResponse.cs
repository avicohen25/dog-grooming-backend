namespace DogGrooming.Models.Response
{
    public class LoginResponse
    {
        public bool isSuccess { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }
}
