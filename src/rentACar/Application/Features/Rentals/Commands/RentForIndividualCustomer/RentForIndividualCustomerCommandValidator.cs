using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.RentForIndividualCustomer
{ 

    public class RentForIndividualCustomerCommandValidator : AbstractValidator<RentForIndividualCustomerCommand>
    {
        public RentForIndividualCustomerCommandValidator()
        {
            RuleFor(c => c.RentDate).NotEmpty();
        }
    }
}
