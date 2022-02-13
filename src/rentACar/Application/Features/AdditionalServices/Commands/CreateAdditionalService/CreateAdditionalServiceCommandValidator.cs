using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Commands.CreateAdditionalService
{
    public class CreateAdditionalServiceCommandValidator : AbstractValidator<CreateAdditionalServiceCommand>
    {
        public CreateAdditionalServiceCommandValidator()
        {
            RuleFor(c => c.ImageUrl).NotEmpty();
            RuleFor(c => c.ServiceType).NotEmpty();
            RuleFor(c => c.ServiceType).IsInEnum();
            RuleFor(c => c.Price).NotEmpty();
            RuleFor(c => c.ServicePoint).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
        }
    }
}
