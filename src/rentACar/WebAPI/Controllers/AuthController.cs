using Application.Features.Users.Commands.LoginUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async ActionResult Login(LoginUserCommand command)
        {
            var userToLogin = await Mediator.Send(command);

            if (userToLogin.LoginIsSuccessful)
            {
                var result = _authService.CreateAccessToken(userToLogin.Data);
            }
            
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
