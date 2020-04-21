using System.Linq;
using AutoMapper;
using EventmanagerApi.Contracts.V1.Responses;
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
        }
    }
}