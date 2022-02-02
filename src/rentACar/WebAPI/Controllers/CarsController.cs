using Application.Features.Cars.Commands.CreateCar;
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
    }
}
