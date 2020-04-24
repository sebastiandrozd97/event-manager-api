namespace EventmanagerApi.Contracts.V1.Requests.UserRequests
{
    public class UserLoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}