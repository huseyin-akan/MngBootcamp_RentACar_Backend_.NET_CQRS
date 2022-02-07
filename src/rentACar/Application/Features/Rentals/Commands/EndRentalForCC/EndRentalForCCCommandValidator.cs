using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.EndRentalForCC
{
    public class EndRentalForCCCommandValidator : AbstractValidator<EndRentalForCCCommand>
    {
        public EndRentalForCCCommandValidator()
        {
            RuleFor(c => c.ReturnedDate).NotEmpty();
            RuleFor(c => c.ReturnedKilometer).NotEmpty();
        }
    }
}
