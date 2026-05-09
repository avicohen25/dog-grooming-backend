namespace DogGrooming.Models.Response
{
    public class RegisterResponse
    {
        public bool isSuccess { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }
}
