using Application.Features.PromotionCodes.Commands.CreatePromotionCode;
using Application.Features.PromotionCodes.Commands.UpdatePromotionCode;
using Application.Features.PromotionCodes.Queries.GetPromotionCode;
using Core.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionCodesController : BaseController
    {
        private IHttpContextAccessor _httpContextAccessor;
        private int userId;

        public PromotionCodesController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreatePromotionCodeCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePromotionCodeCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetPromotionCodeListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("get") ]
        public async Task<IActionResult> GetAll([FromQuery] GetPromotionCodeQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
