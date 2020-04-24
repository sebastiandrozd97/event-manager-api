using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EventmanagerApi.Domain;

namespace EventmanagerApi.Contracts.V1.Responses.EventResponses
{
    public class OrganizedEventResponse
    {
        public Guid Id { get; set; }
        
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
        
        public string UserId { get; set; }
        
        public IEnumerable<ExpenseResponse> Expenses { get; set; }
        
        public IEnumerable<ParticipantResponse> Participants { get; set; }
    }
}