using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(c => c.RegisterDto.FirstName).NotEmpty();
            RuleFor(c => c.RegisterDto.LastName).NotEmpty();
            RuleFor(c => c.RegisterDto.Password).NotEmpty();
            RuleFor(c => c.RegisterDto.Email).NotEmpty();
            RuleFor(c => c.RegisterDto.Email).EmailAddress();
            RuleFor(c => c.RegisterDto.Password).Length(6,20);
        }
    }
}
