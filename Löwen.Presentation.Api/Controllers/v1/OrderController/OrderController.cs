
using Löwen.Application.Features.UserFeature.Commands.AddOrder.AddOrder;
using Löwen.Application.Features.UserFeature.Commands.UpdateOrderItem.UpdateOrderItem;
using Löwen.Application.Features.UserFeature.Commands.UpdateOrderSataus.UpdateOrderStatus;
using Löwen.Application.Features.UserFeature.Queries.GetOrderDetails;
using Löwen.Presentation.Api.Controllers.v1.OrderController.Models;

namespace Löwen.Presentation.Api.Controllers.v1.OrderController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Order")]
    public class OrderController(ISender sender) : ControllerBase
    {
        [HttpPost("add-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrder([FromBody] OrderItemModel model)
        {
            if(model is null)
                return Result.Failure(new Error("api/Order/add-order", "no order items found", ErrorType.BadRequest)).ToActionResult();

            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(id))
                return Result.Failure(new Error("api/Order/add-order", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result result = await sender.Send(new AddOrderCommand(id, model.Items));

            return result.ToActionResult();
        }

        [HttpPut("update-order-status/{Id},{Status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderStatuse(string Id,OrderStatus Status)
        {
            Result result = await sender.Send(new UpdateOrderStatusCommand(Id, Status));

            return result.ToActionResult();
        }


        [HttpPut("update-order-items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderDetails([FromBody] UpdateOrderItemModel model)
        {
            Result result = await sender.Send(new UpdateOrderItemCommand(model.OrderId, model.ProductId, model.Quantity, model.PriceAtPurchase));

            return result.ToActionResult();
        }


        [HttpGet("get-order-details/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderDetails(string Id)
        {
            Result<GetOrderDetailsQueryResponse> result = await sender.Send(new GetOrderDetailsQuery(Id));

            return result.ToActionResult();
        }

        [HttpGet("get-orders-by-user/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrdersByUser(string Id)
        {
            Result<GetOrderDetailsQueryResponse> result = await sender.Send(new GetOrderDetailsQuery(Id));

            return result.ToActionResult();
        }

        [HttpGet("get-orders-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrdersPaged()
        {
            throw new NotImplementedException();
        }

    }
}
