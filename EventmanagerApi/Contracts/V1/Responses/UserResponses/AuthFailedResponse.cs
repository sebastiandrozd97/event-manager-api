using System.Collections.Generic;

namespace EventmanagerApi.Contracts.V1.Responses.UserResponses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}