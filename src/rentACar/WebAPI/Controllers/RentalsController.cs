using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.Rentals.Commands.CreateRental;
using Application.Features.Rentals.Commands.RentForCorporateCustomer;
using Application.Features.Rentals.Commands.RentForIndividualCustomer;
using Application.Features.Rentals.Commands.UpdateRental;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : BaseController
    {

        [HttpPost("rentforindividual")]
        public async Task<IActionResult> RentForIndividual([FromBody] RentForIndividualCustomerCommand command)
        {
            var result = await Mediator.Send(command);

            UpdateCarStateCommand updateCarStateCommand = new UpdateCarStateCommand
            {
                Id = command.CarId,
                CarState = CarState.Rented
            };
            await Mediator.Send(updateCarStateCommand);

            return Created("", result);
        }

        [HttpPost("rentforcorporate")]
        public async Task<IActionResult> RentForCorporate([FromBody] RentForCorporateCustomerCommand command)
        {
            var result = await Mediator.Send(command);

            UpdateCarStateCommand updateCarStateCommand = new UpdateCarStateCommand
            {
                Id = command.CarId,
                CarState = CarState.Rented
            };
            await Mediator.Send(updateCarStateCommand);

            return Created("", result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRentalCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
    }
}
