using Löwen.Application.Features.OrderFeature.Commands.UpdateOrderSataus.UpdateOrderStatus;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.DeliveryController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Delivery")]
    public class DeliveryController(ISender sender) : ControllerBase
    {
        [HttpGet("get-assigned-orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignedOrders()
        {
            throw new NotImplementedException();
        }

        [HttpPut("update-order-status/{Id},{Status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderStatuse(string Id, OrderStatus Status)
        {
            Result result = await sender.Send(new UpdateOrderStatusCommand(Id, Status));

            return result.ToActionResult();
        }


    }
}
