using System;
using System.Collections.Generic;
using EventmanagerApi.Domain;

namespace EventmanagerApi.Contracts.V1.Requests.EventRequests
{
    public class CreateOrganizedEventRequest
    {
        public string Title { get; set; }
        public IEnumerable<CreateExpenseRequest> Expenses { get; set; }
        public IEnumerable<CreateParticipantRequest> Participants { get; set; }
    }
}