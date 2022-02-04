using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Commands.UpdateFuel
{
    public class UpdateFuelCommandValidator : AbstractValidator<UpdateFuelCommand>
    {
        public UpdateFuelCommandValidator()
        {
            RuleFor(f => f.Id).NotEmpty();
            RuleFor(f => f.Name).NotEmpty();
            RuleFor(f => f.Name).Length(3, 15).WithMessage("Yakıt tipi en az 3 en fazla 15 karakter olmalıdır.");
        }
    }
}
