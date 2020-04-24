namespace EventmanagerApi.Contracts.V1.Requests.UserRequests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}