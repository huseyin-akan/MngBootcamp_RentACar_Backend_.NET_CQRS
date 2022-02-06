using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarStateCommandValidator : AbstractValidator<UpdateCarStateCommand>
    {
        public UpdateCarStateCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.CarState).IsInEnum()
                .WithMessage("Araç durumu hatalı. Böyle bir araç durumu yok");
        }

        private bool BeNewCar(int modelYear)
        {
            return modelYear >= 2015;
        }

        private bool StartWithValidNumber(string plate)
        {
            var firstTwo = plate.Substring(0, 2);
            int tmp = 0;
            bool result = int.TryParse(firstTwo, out tmp);
            bool validNumber = tmp <= 81;
            return result && validNumber;
        }
    }
}
