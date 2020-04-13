using System;
using System.Collections.Generic;
using EventmanagerApi.Contracts;
using EventmanagerApi.Contracts.V1;
using EventmanagerApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EventmanagerApi.Controllers
{
    [ApiController]
    public class OrganizedEventsController : ControllerBase
    {
        private List<OrganizedEvent> _organizedEvents;

        public OrganizedEventsController()
        {
            _organizedEvents = new List<OrganizedEvent>();
            for (int i = 0; i < 5; i++)
            {
                _organizedEvents.Add(new OrganizedEvent{ Id = Guid.NewGuid()});
            }
        }
        
        [HttpGet]
        [Route(ApiRoutes.OrganizedEvents.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_organizedEvents);
        }
    }
}