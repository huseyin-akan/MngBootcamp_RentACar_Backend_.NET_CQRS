using Application.Features.CarDamages.Commands.CreateCarDamage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDamagesController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCarDamageCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
    }
}
