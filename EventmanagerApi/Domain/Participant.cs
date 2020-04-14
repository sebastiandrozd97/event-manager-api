using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventmanagerApi.Domain
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        public string Status { get; set; }
        
        [ForeignKey("EventId")]
        public OrganizedEvent OrganizedEvent { get; set; }

        public Guid EventId { get; set; }
    }
}