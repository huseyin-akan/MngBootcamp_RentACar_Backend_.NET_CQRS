using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.RegisterUpdateDto.FirstName).NotEmpty();
            RuleFor(c => c.RegisterUpdateDto.LastName).NotEmpty();
            RuleFor(c => c.RegisterUpdateDto.Password).NotEmpty();
            RuleFor(c => c.RegisterUpdateDto.Email).NotEmpty();
            RuleFor(c => c.RegisterUpdateDto.Email).EmailAddress();
        }
    }
}
