using System;
using System.Collections.Generic;
using System.Linq;
using EventmanagerApi.Domain;

namespace EventmanagerApi.Services
{
    public class OrganizedEventService : IOrganizedEventService
    {
        private readonly List<OrganizedEvent> _organizedEvents;

        public OrganizedEventService()
        {
            _organizedEvents = new List<OrganizedEvent>();
            for (int i = 0; i < 5; i++)
            {
                _organizedEvents.Add(new OrganizedEvent
                {
                    Id = Guid.NewGuid(),
                    Title = $"Event title {i+1}"
                });
            }
        }
        
        public List<OrganizedEvent> GetEvents()
        {
            return _organizedEvents;
        }

        public OrganizedEvent GetEventById(Guid eventId)
        {
            return _organizedEvents.SingleOrDefault(x => x.Id == eventId);
        }

        public bool UpdateEvent(OrganizedEvent eventToUpdate)
        {
            var exists = GetEventById(eventToUpdate.Id) != null;

            if (!exists)
            {
                return false;
            }

            var index = _organizedEvents.FindIndex(x => x.Id == eventToUpdate.Id);
            _organizedEvents[index] = eventToUpdate;
            return true;
        }

        public bool DeleteEvent(Guid eventId)
        {
            var organizedEvent = GetEventById(eventId);

            if (organizedEvent == null)
            {
                return false;
            }
            
            _organizedEvents.Remove(organizedEvent);
            return true;
        }
    }
}