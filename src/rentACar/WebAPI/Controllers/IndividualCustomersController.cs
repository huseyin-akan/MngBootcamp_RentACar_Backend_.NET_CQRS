using Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/individualcustomers")]
    [ApiController]
    public class IndividualCustomersController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateIndividualCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
    }
}
