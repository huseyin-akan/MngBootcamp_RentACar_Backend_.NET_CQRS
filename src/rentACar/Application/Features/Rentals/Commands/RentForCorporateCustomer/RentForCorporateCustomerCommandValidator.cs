using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.RentForCorporateCustomer
{
    public class RentForCorporateCustomerCommandValidator : AbstractValidator<RentForCorporateCustomerCommand>
    {
        public RentForCorporateCustomerCommandValidator()
        {
            RuleFor(c => c.RentDate).NotEmpty();
            RuleFor(c => c.RentedKilometer).NotEmpty();
        }
    }
}
