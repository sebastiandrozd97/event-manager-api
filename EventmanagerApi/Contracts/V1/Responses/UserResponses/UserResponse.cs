using Microsoft.EntityFrameworkCore.Design;

namespace EventmanagerApi.Contracts.V1.Responses.UserResponses
{
    public class UserResponse
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Gender { get; set; }

        public string Email { get; set; }
    }
}