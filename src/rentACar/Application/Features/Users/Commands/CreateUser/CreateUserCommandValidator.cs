using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
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
