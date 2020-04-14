using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventmanagerApi.Data;
using EventmanagerApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventmanagerApi.Services
{
    public class OrganizedEventService : IOrganizedEventService
    {
        private readonly DataContext _dataContext;

        public OrganizedEventService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<List<OrganizedEvent>> GetEventsAsync()
        {
            return await _dataContext.OrganizedEvents.ToListAsync();
        }

        public async Task<OrganizedEvent> GetEventByIdAsync(Guid eventId)
        {
            return await _dataContext.OrganizedEvents.SingleOrDefaultAsync(x => x.Id == eventId);
        }

        public async Task<bool> CreateEventAsync(OrganizedEvent organizedEvent)
        {
            await _dataContext.OrganizedEvents.AddAsync(organizedEvent);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateEventAsync(OrganizedEvent eventToUpdate)
        {
            _dataContext.OrganizedEvents.Update(eventToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteEventAsync(Guid eventId)
        {
            var organizedEvent = await GetEventByIdAsync(eventId);

            if (organizedEvent == null)
            {
                return false;
            }

            _dataContext.OrganizedEvents.Remove(organizedEvent);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}