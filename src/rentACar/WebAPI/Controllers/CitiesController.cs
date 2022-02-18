using Application.Features.Cities.Queries.GetCity;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : BaseController
    {
        //TODO:
        //[HttpPost("add")]
        //public async Task<IActionResult> Add([FromBody] CreateCityController command)
        //{
        //    var result = await Mediator.Send(command);
        //    return Created("", result);
        //}

        //[HttpPost("update")]
        //public async Task<IActionResult> Update([FromBody] UpdateColorCommand command)
        //{
        //    var result = await Mediator.Send(command);
        //    return Created("", result);
        //}

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetCityListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
