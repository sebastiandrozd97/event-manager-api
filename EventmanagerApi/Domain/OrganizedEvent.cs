using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventmanagerApi.Domain
{
    public class OrganizedEvent
    {
        [Key]
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
        
        public ICollection<Expense> Expenses { get; set; }
            = new List<Expense>();
        
        public ICollection<Participant> Participants { get; set; }
            = new List<Participant>();

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}