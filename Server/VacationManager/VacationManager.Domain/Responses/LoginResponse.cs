namespace VacationManager.Domain.Responses
{
    using System.Text.Json.Serialization;

    public class LoginResponse : Response
    {
        public LoginResponse()
        {

        }

        public LoginResponse(string email, string token)
        {
            this.Email = email;
            this.JWT = token;
            //this.RefreshToken = refreshToken;
        }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("JWT")]
        public string JWT { get; set; }

        //[JsonIgnore]  refresh token is returned in http only cookie
        //public string RefreshToken { get; set; }
    }
}
