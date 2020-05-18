using AutoMapper;
using EventmanagerApi.Contracts.V1.Responses.EventResponses;
using EventmanagerApi.Contracts.V1.Responses.UserResponses;
using EventmanagerApi.Domain;

namespace EventmanagerApi.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<OrganizedEvent, OrganizedEventResponse>();
            CreateMap<Expense, ExpenseResponse>();
            CreateMap<Participant, ParticipantResponse>();
            CreateMap<ApplicationUser, UserResponse>();
        }
    }
}