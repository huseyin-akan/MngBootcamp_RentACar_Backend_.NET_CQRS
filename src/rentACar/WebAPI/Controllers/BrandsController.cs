using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.GetBrand;
using Core.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        private IHttpContextAccessor _httpContextAccessor;
        private int userId;

        public BrandsController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpGet("getall"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            string? authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
            if (authHeader == null)
            {
                userId = 0;
            }
            else
            {
                userId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            var query = new GetBrandListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
