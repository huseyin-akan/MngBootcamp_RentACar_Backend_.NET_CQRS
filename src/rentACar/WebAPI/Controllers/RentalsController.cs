using Application.Features.Rentals.Commands.CreateRental;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddMaintenance([FromBody] CreateRentalCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
    }
}
