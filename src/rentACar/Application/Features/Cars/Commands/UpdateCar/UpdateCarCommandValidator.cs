using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.Plate).NotEmpty();
            RuleFor(c => c.ModelId).NotEmpty();
            RuleFor(c => c.ColorId).NotEmpty();
            RuleFor(c => c.FindexScore).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1900);
            RuleFor(c => c.ColorId).GreaterThan(0);
            RuleFor(c => c.ModelId).GreaterThan(0);
            RuleFor(c => c.CarState).IsInEnum()
                .WithMessage("Araç durumu hatalı. Böyle bir araç durumu yok");
            RuleFor(c => c.Plate).Length(6, 9);
            RuleFor(c => c.Plate).Must(StartWithValidNumber)
                .WithMessage("Araç plakası geçerli bir şehir numarasıyla başlamıyor!");
            RuleFor(c => c.ModelYear).Must(BeNewCar)
                .WithMessage("Eklemek istediğiniz araç modeli eski olmamalı!");
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
