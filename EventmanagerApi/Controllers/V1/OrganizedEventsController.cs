using System;
using System.Linq;
using System.Threading.Tasks;
using EventmanagerApi.Contracts.V1;
using EventmanagerApi.Contracts.V1.Requests;
using EventmanagerApi.Contracts.V1.Responses;
using EventmanagerApi.Domain;
using EventmanagerApi.Extensions;
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
            var organizedEvents = await _organizedEventService.GetEventsAsync(HttpContext.GetUserId());
            var organizedEventResponses = organizedEvents.Select(organizedEvent => new OrganizedEventResponse
            {
                Id = organizedEvent.Id,
                Title = organizedEvent.Title,
                Expenses = organizedEvent.Expenses.Select(x => new ExpenseResponse {Name = x.Name, Cost = x.Cost}),
                Participants = organizedEvent.Participants.Select(x => new ParticipantResponse
                    {Name = x.Name, Status = x.Status}),
                UserId = organizedEvent.UserId
            }).ToList();
            return Ok(organizedEventResponses);
        }
        
        [HttpGet(ApiRoutes.OrganizedEvents.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid eventId)
        {
            var organizedEvent = await _organizedEventService.GetEventByIdAsync(eventId);

            if (organizedEvent == null)
            {
                return NotFound();
            }
            
            return Ok(new OrganizedEventResponse
            {
                Id = organizedEvent.Id,
                Title = organizedEvent.Title,
                Expenses = organizedEvent.Expenses.Select(x => new ExpenseResponse{Name = x.Name, Cost = x.Cost}),
                Participants = organizedEvent.Participants.Select(x => new ParticipantResponse{Name = x.Name, Status = x.Status}),
                UserId = organizedEvent.UserId
            });
        }

        [HttpPost(ApiRoutes.OrganizedEvents.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOrganizedEventRequest organizedEventRequest)
        {
            var newEventId = Guid.NewGuid();
            var organizedEvent = new OrganizedEvent
            {
                Id = newEventId,
                Title = organizedEventRequest.Title,
                UserId = HttpContext.GetUserId(),
                Expenses = organizedEventRequest.Expenses.Where(x => !string.IsNullOrWhiteSpace(x.Name)).Select(x => new Expense{EventId = newEventId, Name = x.Name, Cost = x.Cost}).ToList(),
                Participants = organizedEventRequest.Participants.Where(x => !string.IsNullOrWhiteSpace(x.Name)).Select((x,y) => new Participant{EventId = newEventId, Name = x.Name, Status = x.Status}).ToList()
            };

            await _organizedEventService.CreateEventAsync(organizedEvent);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.OrganizedEvents.Get.Replace("{eventId}", organizedEvent.Id.ToString());

            var response = new OrganizedEventResponse
            {
                Id = organizedEvent.Id,
                Title = organizedEvent.Title,
                Expenses = organizedEvent.Expenses.Select(x => new ExpenseResponse{Name = x.Name, Cost = x.Cost}),
                Participants = organizedEvent.Participants.Select(x => new ParticipantResponse{Name = x.Name, Status = x.Status}),
                UserId = organizedEvent.UserId
            };
            return Created(locationUri, response);
        }
        
        [HttpPut(ApiRoutes.OrganizedEvents.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid eventId, [FromBody] UpdateOrganizedEventRequest request)
        {
            var userOwnsEvent = await _organizedEventService.UserOwnsEventAsync(eventId, HttpContext.GetUserId());

            if (!userOwnsEvent)
            {
                return BadRequest(new {error = "You do not own this post"});
            }

            var organizedEvent = await _organizedEventService.GetEventByIdAsync(eventId);
            organizedEvent.Title = request.Title;

            var updated = await _organizedEventService.UpdateEventAsync(organizedEvent);

            if (updated)
            {
                return Ok(new OrganizedEventResponse
                {
                    Id = organizedEvent.Id,
                    Title = organizedEvent.Title,
                    Expenses = organizedEvent.Expenses.Select(x => new ExpenseResponse{Name = x.Name, Cost = x.Cost}),
                    Participants = organizedEvent.Participants.Select(x => new ParticipantResponse{Name = x.Name, Status = x.Status}),
                    UserId = organizedEvent.UserId
                });
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.OrganizedEvents.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid eventId)
        {
            var userOwnsEvent = await _organizedEventService.UserOwnsEventAsync(eventId, HttpContext.GetUserId());

            if (!userOwnsEvent)
            {
                return BadRequest(new {error = "You do not own this post"});
            }
            
            var deleted = await _organizedEventService.DeleteEventAsync(eventId);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}