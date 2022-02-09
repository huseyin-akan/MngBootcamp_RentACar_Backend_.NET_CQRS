using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenenaces.Commands.UpdateMaintenance
{
    public class UpdateMaintenanceCommandValidator : AbstractValidator<UpdateMaintenanceCommand>
    {
        public UpdateMaintenanceCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.Description).MaximumLength(500);
        }
    }
}
