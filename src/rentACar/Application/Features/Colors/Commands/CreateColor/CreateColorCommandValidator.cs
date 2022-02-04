using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Commands.CreateColor
{
    public class CreateColorCommandValidator :AbstractValidator<CreateColorCommand>
    {
        public CreateColorCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).Must(NotContainW).WithMessage("İçerisinde w olan renk olamaz!!");
        }

        private bool NotContainW(string name)
        {
            return !name.Contains('w');
        }
    }
}
