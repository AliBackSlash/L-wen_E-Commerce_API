using Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;
using Löwen.Application.Features.OrderFeature.Commands.UpdateOrderSataus.UpdateOrderStatus;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.DeliveryController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Delivery")]
    public class DeliveryController(ISender sender) : ControllerBase
    {
        [HttpGet("get-assigned-orders/{PageNumber},{PageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignedOrders(int PageNumber, byte PageSize)
        {
            var Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(Id))
                return Result.Failure(new Error("api/Order/add-order", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result<PagedResult<GetAssignedOrdersQueryresponse>> result = await sender.Send(new GetAssignedOrdersQuery(Id,PageNumber, PageSize));

            return result.ToActionResult();
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
