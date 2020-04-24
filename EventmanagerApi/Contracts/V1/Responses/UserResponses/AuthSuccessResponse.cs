namespace EventmanagerApi.Contracts.V1.Responses.UserResponses
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}