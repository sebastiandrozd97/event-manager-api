using System.ComponentModel.DataAnnotations;

namespace EventmanagerApi.Contracts.V1.Requests.UserRequests
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}