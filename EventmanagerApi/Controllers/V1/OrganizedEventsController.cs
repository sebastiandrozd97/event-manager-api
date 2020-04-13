using System;
using EventmanagerApi.Contracts.V1;
using EventmanagerApi.Contracts.V1.Requests;
using EventmanagerApi.Contracts.V1.Responses;
using EventmanagerApi.Domain;
using EventmanagerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventmanagerApi.Controllers.V1
{
    [ApiController]
    public class OrganizedEventsController : ControllerBase
    {
        private readonly IOrganizedEventService _organizedEventService;
        
       public OrganizedEventsController(IOrganizedEventService organizedEventService)
       {
           _organizedEventService = organizedEventService;
       }
        
        [HttpGet(ApiRoutes.OrganizedEvents.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_organizedEventService.GetEvents());
        }
        
        [HttpGet(ApiRoutes.OrganizedEvents.Get)]
        public IActionResult Get([FromRoute] Guid eventId)
        {
            var organizedEvent = _organizedEventService.GetEventById(eventId);

            if (organizedEvent == null)
            {
                return NotFound();
            }
            
            return Ok(organizedEvent);
        }

        [HttpPost(ApiRoutes.OrganizedEvents.Create)]
        public IActionResult Create([FromBody] CreateOrganizedEventRequest organizedEventRequest)
        {
            var organizedEvent = new OrganizedEvent{Id = organizedEventRequest.Id};
            
            if (organizedEvent.Id != Guid.Empty)
            {
                organizedEvent.Id = Guid.NewGuid();
            }
            
            _organizedEventService.GetEvents().Add(organizedEvent);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.OrganizedEvents.Get.Replace("{eventId}", organizedEvent.Id.ToString());

            var response = new OrganizedEventResponse {Id = organizedEvent.Id};
            return Created(locationUri, response);
        }
        
        [HttpPut(ApiRoutes.OrganizedEvents.Update)]
        public IActionResult Update([FromRoute] Guid eventId, [FromBody] UpdateOrganizedEventRequest request)
        {
            var organizedEvent = new OrganizedEvent
            {
                Id = eventId,
                Title = request.Title
            };

            var updated = _organizedEventService.UpdateEvent(organizedEvent);

            if (updated)
            {
                return Ok(organizedEvent);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.OrganizedEvents.Delete)]
        public IActionResult Delete([FromRoute] Guid eventId)
        {
            var deleted = _organizedEventService.DeleteEvent(eventId);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}