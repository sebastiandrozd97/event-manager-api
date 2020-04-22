using System.Linq;
using EventmanagerApi.Contracts.V1.Requests;
using FluentValidation;

namespace EventmanagerApi.Validators
{
    public class CreateExpenseRequestValidator : AbstractValidator<CreateOrganizedEventRequest>
    {
        public CreateExpenseRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }
}