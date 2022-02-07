using Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporateCustomersController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCorporateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
    }
}
