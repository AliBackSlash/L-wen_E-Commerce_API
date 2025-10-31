
using Löwen.Application.Features.OrderFeature.Commands.AddOrder.AddOrder;
using Löwen.Application.Features.OrderFeature.Commands.UpdateOrderItem.UpdateOrderItem;
using Löwen.Application.Features.OrderFeature.Queries.GetAllOrders;
using Löwen.Application.Features.OrderFeature.Queries.GetOrderDetails;
using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Presentation.Api.Controllers.v1.OrderController.Models;

namespace Löwen.Presentation.Api.Controllers.v1.OrderController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Order")]
    //make sure use send order status email every oper
    public class OrderController(ISender sender) : ControllerBase
    {
        [HttpPost("add-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrder([FromBody] OrderItemModel model)
        {
            var CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(CustomerId))
                return Result.Failure(new Error("api/Order/add-order", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result result = await sender.Send(new AddOrderCommand(CustomerId, model.orders));
            return result.ToActionResult();
        }

        [HttpPut("update-order-items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderDetails([FromBody] UpdateOrderItemModel model)
        {
            Result result = await sender.Send(new UpdateOrderItemCommand(model.OrderId,model.deliveryId, model.ProductId, model.Quantity, model.Price));

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

        /*        [HttpGet("get-orders-by-user/{PageNumber},{PageSize}")]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
                [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
                public async Task<IActionResult> GetOrdersByUser(int PageNumber, byte PageSize)
                {
                    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (string.IsNullOrEmpty(id))
                        return Result.Failure(new Error("api/Order/get-orders-by-user", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

                    Result<PagedResult<GetOrderDetailsQueryResponse>> result = await sender.Send(new GetOrdersForUserQuery(id, PageNumber, PageSize));

                    return result.ToActionResult();
                }*/

        [HttpGet("get-orders-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrdersPaged(int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetOrderDetailsQueryResponse>> result = await sender.Send(new GetAllOrdersQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }

    }
}
