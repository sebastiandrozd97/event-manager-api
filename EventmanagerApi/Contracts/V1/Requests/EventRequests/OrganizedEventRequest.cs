using System;
using System.Collections.Generic;

namespace EventmanagerApi.Contracts.V1.Requests.EventRequests
{
    public class OrganizedEventRequest
    {
        public string Title { get; set; }
        
        public string Slug { get; set; }

        public string Description { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public bool LastsOneDay { get; set; }

        public string Address { get; set; }

        public string Place { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }
        public IEnumerable<ExpenseRequest> Expenses { get; set; }
        public IEnumerable<ParticipantRequest> Participants { get; set; }
    }
}