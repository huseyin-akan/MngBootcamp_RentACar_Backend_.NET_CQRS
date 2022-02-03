using Application.Features.Fuels.Commands.CreateFuel;
using Application.Features.Fuels.Commands.UpdateFuel;
using Application.Features.Fuels.Queries.GetFuel;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelsController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateFuelCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateFuelCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetFuelListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
