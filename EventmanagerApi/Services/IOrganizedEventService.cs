using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventmanagerApi.Domain;

namespace EventmanagerApi.Services
{
    public interface IOrganizedEventService
    {
        Task<List<OrganizedEvent>> GetEventsAsync();

        Task<OrganizedEvent> GetEventByIdAsync(Guid eventId);

        Task<bool> CreateEventAsync(OrganizedEvent organizedEvent);

        Task<bool> UpdateEventAsync(OrganizedEvent eventToUpdate);
        
        Task<bool> DeleteEventAsync(Guid eventId);
        Task<bool> UserOwnsEventAsync(Guid eventId, string userId);
    }
}