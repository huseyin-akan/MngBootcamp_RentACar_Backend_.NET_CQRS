using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenenaces.Commands.CreateMaintenance
{
    public class CreateMaintenanceCommandValidator : AbstractValidator<CreateMaintenanceCommand>
    {
        public CreateMaintenanceCommandValidator()
        {
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(500);
        }
    }
}
