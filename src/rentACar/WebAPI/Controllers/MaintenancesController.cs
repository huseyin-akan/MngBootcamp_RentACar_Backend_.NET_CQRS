using Application.Features.Maintenenaces.Commands.CreateMaintenance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenancesController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddMaintenance([FromBody] CreateMaintenanceCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
    }
}
