namespace E_Commerce_API.Model
{
    public class LoginResponseModel
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; } // This will hold the JWT Token
    }
}
