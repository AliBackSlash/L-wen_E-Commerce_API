using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.DeliveryController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        [HttpGet("get-assigned-orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignedOrders()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-order-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderById()
        {
            throw new NotImplementedException();
        }

        [HttpPut("update-order-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderStatus()
        {
            throw new NotImplementedException();
        }

    }
}
