using Application.Features.AdditionalServices.Commands.CreateAdditionalService;
using Application.Features.AdditionalServices.Commands.UpdateAdditionalService;
using Application.Features.AdditionalServices.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalServicesController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateAdditionalServiceCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAdditionalServiceCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetAdditionalServiceListQuery();
            query.PageRequest = pageRequest;

            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
