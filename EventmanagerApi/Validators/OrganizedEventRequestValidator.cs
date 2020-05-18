using EventmanagerApi.Contracts.V1.Requests.EventRequests;
using FluentValidation;

namespace EventmanagerApi.Validators
{
    public class OrganizedEventRequestValidator : AbstractValidator<OrganizedEventRequest>
    {
        public OrganizedEventRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }
}