using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.Maintenenaces.Commands.CreateMaintenance;
using Application.Features.Maintenenaces.Commands.UpdateMaintenance;
using Application.Features.Maintenenaces.Queries.GetMaintenance;
using Core.Application.Requests;
using Domain.Enums;
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

            UpdateCarStateCommand updateCarStateCommand = new UpdateCarStateCommand
            {
                Id = command.CarId,
                CarState = CarState.InMaintenance
            };
            await Mediator.Send(updateCarStateCommand);

            return Created("", result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateMaintenanceCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetMaintenanceListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
