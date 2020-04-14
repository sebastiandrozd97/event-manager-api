using System;
using System.ComponentModel.DataAnnotations;

namespace EventmanagerApi.Domain
{
    public class OrganizedEvent
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
    }
}