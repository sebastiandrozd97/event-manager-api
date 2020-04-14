using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventmanagerApi.Domain
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Gender { get; set; }
        
        public ICollection<OrganizedEvent> OrganizedEvents { get; set; }
            = new List<OrganizedEvent>();
    }
}