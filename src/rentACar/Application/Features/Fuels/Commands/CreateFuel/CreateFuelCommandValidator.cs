using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Commands.CreateFuel
{
    public class CreateFuelCommandValidator :AbstractValidator<CreateFuelCommand>
    {
        public CreateFuelCommandValidator()
        {
            RuleFor(f => f.Name).NotEmpty();
            RuleFor(f => f.Name).Length(3, 15).WithMessage("Yakıt tipi en az 3 en fazla 15 karakter olmalıdır.");
        }
    }
}
