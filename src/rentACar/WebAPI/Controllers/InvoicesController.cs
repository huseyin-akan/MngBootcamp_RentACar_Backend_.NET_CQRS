using Application.Features.Invoices.Queries.GetInvoice;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : BaseController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllInvoices([FromQuery] PageRequest pageRequest)
        {
            var query = new GetInvoiceListQuery();
            query.PageRequest = pageRequest;
            
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getallbycustomerid")]
        public async Task<IActionResult> GetAllInvoicesByCustomerId([FromQuery] PageRequest pageRequest,
            [FromQuery] int customerId)
        {
            var query = new GetInvoiecListByCustomerIdQuery();
            query.PageRequest = pageRequest;
            query.CustomerId = customerId;

            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getallbetweendates")]
        public async Task<IActionResult> GetAllInvoicesBetweenDates([FromQuery] PageRequest pageRequest,
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetInvoiceListBetweenDatesQuery();
            query.PageRequest = pageRequest;
            query.StartDate = startDate;
            query.EndDate = endDate;

            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
