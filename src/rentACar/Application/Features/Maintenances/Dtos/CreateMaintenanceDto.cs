using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenenaces.Dtos
{
    public class CreateMaintenanceDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public int CarId { get; set; }
    }
}
