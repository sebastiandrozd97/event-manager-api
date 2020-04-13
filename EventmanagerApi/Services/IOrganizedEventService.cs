using System;
using System.Collections.Generic;
using EventmanagerApi.Domain;

namespace EventmanagerApi.Services
{
    public interface IOrganizedEventService
    {
        List<OrganizedEvent> GetEvents();

        OrganizedEvent GetEventById(Guid eventId);

        bool UpdateEvent(OrganizedEvent eventToUpdate);
        
        bool DeleteEvent(Guid eventId);
    }
}