using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.EndRentalForIC
{
    public class EndRentalForICCommandValidator : AbstractValidator<EndRentalForICCommand>
    {
        public EndRentalForICCommandValidator()
        {
            RuleFor(c => c.ReturnedDate).NotEmpty();
            RuleFor(c => c.ReturnedKilometer).NotEmpty();
        }
    }
}
