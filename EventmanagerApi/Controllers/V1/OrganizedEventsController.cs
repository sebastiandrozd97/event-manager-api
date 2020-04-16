using System;
using System.Threading.Tasks;
using EventmanagerApi.Contracts.V1;
using EventmanagerApi.Contracts.V1.Requests;
using EventmanagerApi.Contracts.V1.Responses;
using EventmanagerApi.Domain;
using EventmanagerApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventmanagerApi.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrganizedEventsController : ControllerBase
    {
        private readonly IOrganizedEventService _organizedEventService;
        
       public OrganizedEventsController(IOrganizedEventService organizedEventService)
       {
           _organizedEventService = organizedEventService;
       }
        
        [HttpGet(ApiRoutes.OrganizedEvents.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _organizedEventService.GetEventsAsync());
        }
        
        [HttpGet(ApiRoutes.OrganizedEvents.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid eventId)
        {
            var organizedEvent = await _organizedEventService.GetEventByIdAsync(eventId);

            if (organizedEvent == null)
            {
                return NotFound();
            }
            
            return Ok(organizedEvent);
        }

        [HttpPost(ApiRoutes.OrganizedEvents.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOrganizedEventRequest organizedEventRequest)
        {
            var organizedEvent = new OrganizedEvent{Title = organizedEventRequest.Title};

            await _organizedEventService.CreateEventAsync(organizedEvent);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.OrganizedEvents.Get.Replace("{eventId}", organizedEvent.Id.ToString());

            var response = new OrganizedEventResponse {Id = organizedEvent.Id};
            return Created(locationUri, response);
        }
        
        [HttpPut(ApiRoutes.OrganizedEvents.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid eventId, [FromBody] UpdateOrganizedEventRequest request)
        {
            var organizedEvent = new OrganizedEvent
            {
                Id = eventId,
                Title = request.Title
            };

            var updated = await _organizedEventService.UpdateEventAsync(organizedEvent);

            if (updated)
            {
                return Ok(organizedEvent);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.OrganizedEvents.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid eventId)
        {
            var deleted = await _organizedEventService.DeleteEventAsync(eventId);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}