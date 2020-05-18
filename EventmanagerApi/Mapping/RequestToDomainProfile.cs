using System;
using AutoMapper;
using EventmanagerApi.Contracts.V1.Requests.EventRequests;
using EventmanagerApi.Domain;

namespace EventmanagerApi.Mapping
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<OrganizedEventRequest, OrganizedEvent>()
                .ForMember(
                    dest => dest.Id, 
                    opt => opt
                        .MapFrom(dest => Guid.NewGuid()));

            CreateMap<ExpenseRequest, Expense>();
            CreateMap<ParticipantRequest, Participant>();
        }
        
        
    }
}