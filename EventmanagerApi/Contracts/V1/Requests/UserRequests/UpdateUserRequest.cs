namespace EventmanagerApi.Contracts.V1.Requests.UserRequests
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Gender { get; set; }
    }
}