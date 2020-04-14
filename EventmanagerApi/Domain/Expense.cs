using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventmanagerApi.Domain
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Cost { get; set; }
        
        [ForeignKey("EventId")]
        public OrganizedEvent OrganizedEvent { get; set; }

        public Guid EventId { get; set; }
    }
}