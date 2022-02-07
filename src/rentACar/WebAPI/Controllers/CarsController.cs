using Application.Features.Cars.Commands.CreateCar;
using Application.Features.Cars.Queries.GetCar;
using Application.Features.Maintenenaces.Commands.UpdateMaintenance;
using Application.Features.Maintenenaces.Queries.GetMaintenance;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCarCommand command)
        {
            var result = await Mediator.Send(command);
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
            var query = new GetAllCarsListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getallrentables")]
        public async Task<IActionResult> GetAllRentables([FromQuery] PageRequest pageRequest)
        {
            var query = new GetAllRentableCarsListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getallcarsbycityid")]
        public async Task<IActionResult> GetAllRentables([FromQuery] PageRequest pageRequest, [FromQuery] int cityId)
        {
            var query = new GetAllCarsByCityQuery();
            query.PageRequest = pageRequest;
            query.CityId = cityId;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
