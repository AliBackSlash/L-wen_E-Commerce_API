using Löwen.Application.Features.UserFeature.Commands.Love.AddOrder;
using Löwen.Application.Features.UserFeature.Commands.Love.UpdateOrderItem;
using Löwen.Application.Features.UserFeature.Commands.Love.UpdateOrderStatus;
using Löwen.Domain.Layer_Dtos.Order;
using Löwen.Presentation.Api.Controllers.v1.OrderController.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> GetOrderDetails(Guid Id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-orders-by-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrdersByUser()
        {
            throw new NotImplementedException();
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
