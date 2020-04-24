using System.Linq;
using EventmanagerApi.Contracts.V1.Requests.EventRequests;
using FluentValidation;

namespace EventmanagerApi.Validators
{
    public class CreateOrganizedEventRequestValidator : AbstractValidator<CreateOrganizedEventRequest>
    {
        public CreateOrganizedEventRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }
}