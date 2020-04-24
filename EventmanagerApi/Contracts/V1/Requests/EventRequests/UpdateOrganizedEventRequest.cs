using System.Collections.Generic;

namespace EventmanagerApi.Contracts.V1.Requests.EventRequests
{
    public class UpdateOrganizedEventRequest
    {
        public string Title { get; set; }
        public IEnumerable<CreateExpenseRequest> Expenses { get; set; }
        public IEnumerable<CreateParticipantRequest> Participants { get; set; }
    }
}