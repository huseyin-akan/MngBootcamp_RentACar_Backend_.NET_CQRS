using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.RegisterUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginResult = await Mediator.Send(command);

            return Ok(loginResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var registerResult = await Mediator.Send(command);
            return Created("", registerResult);
        }
    }
}
