using Löwen.Application.Features.CartFeature.Commands.AddToCart;
using Löwen.Application.Features.CartFeature.Commands.RemoveFromCartItem;
using Löwen.Application.Features.CartFeature.Commands.UpdateCartItemQuantity;
using Löwen.Application.Features.CartFeature.Queries.GetCartByUser;
using Löwen.Domain.Entities;
using Löwen.Presentation.Api.Controllers.v1.CartController.Models;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.CartController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Cart")]
    public class CartController(ISender sender) : ControllerBase
    {
        [HttpPost("add-to-cart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToCart([FromBody] CartItemModel model)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(id))
                return Result.Failure(new Error("api/users/get-user-info", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result result = await sender.Send(new AddToCartCommand(id, model.ProductId,model.Quantity));

            return result.ToActionResult();
        }

        [HttpDelete("remove-from-cart/{CartId},{ProductId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFromCart(string CartId, string ProductId)
        {
            Result result = await sender.Send(new RemoveFromCartItemCommand(CartId, ProductId));

            return result.ToActionResult();
        }

        [HttpPut("update-cart-item-quantity/{CartId},{ProductId},{Quantity}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCartItemQuantity(string CartId, string ProductId, short Quantity)
        {
            Result result = await sender.Send(new UpdateCartItemQuantityCommand(CartId, ProductId,Quantity));

            return result.ToActionResult();
        }

        [HttpGet("get-cart-by-user/{PageNumber},{PageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartByUser(int PageNumber, byte PageSize)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(id))
                return Result.Failure(new Error("api/users/get-user-info", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();
            Result<PagedResult<GetCartByUserQueryresponse>> result = await sender.Send(new GetCartByUserQuery(id, PageNumber, PageSize));

            return result.ToActionResult();
        }

    }
}
