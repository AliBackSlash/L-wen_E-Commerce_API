namespace Löwen.Presentation.Api.Controllers.v1.CartController
{
    /// <summary>
    /// API endpoints to manage the current user's shopping cart.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Cart")]
    [Authorize( Roles = "User")]
    public class CartController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Add an item to the authenticated user's cart.
        /// </summary>
        /// <param name="model">The cart item payload containing the product identifier and quantity.</param>
        /// <returns>
        /// Returns 201 Created when the item is added successfully.
        /// Returns 400 Bad Request with a collection of <see cref="Error"/> for validation or client errors.
        /// Returns 401 Unauthorized when the request does not contain a valid user token.
        /// Returns 409 Conflict with a collection of <see cref="Error"/> when a duplicate resource or business conflict occurs.
        /// Returns 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected server errors.
        /// </returns>
        [HttpPost("add-to-cart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToCart([FromBody] CartItemModel model)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(id))
                return Result.Failure(new Error("api/users/get-user-info", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result result = await sender.Send(new AddToCartCommand(id, model.ProductId,model.Quantity));

            return result.ToActionResult();
        }

        /// <summary>
        /// Remove an item from a cart.
        /// </summary>
        /// <param name="CartId">The identifier of the cart containing the item to remove.</param>
        /// <param name="ProductId">The identifier of the product to remove from the cart.</param>
        /// <returns>
        /// Returns 204 No Content when the item was removed successfully.
        /// Returns 400 Bad Request with a collection of <see cref="Error"/> for validation or client errors.
        /// Returns 404 Not Found with a collection of <see cref="Error"/> when the cart or item is not found.
        /// Returns 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected server errors.
        /// </returns>
        [HttpDelete("remove-from-cart/{CartId},{ProductId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFromCart(string CartId, string ProductId)
        {
            Result result = await sender.Send(new RemoveFromCartItemCommand(CartId, ProductId));

            return result.ToActionResult();
        }

        /// <summary>
        /// Update the quantity of an existing cart item.
        /// </summary>
        /// <param name="CartId">The identifier of the cart containing the item.</param>
        /// <param name="ProductId">The identifier of the product whose quantity will be updated.</param>
        /// <param name="Quantity">The new quantity for the specified cart item.</param>
        /// <returns>
        /// Returns 200 OK when the update succeeds.
        /// Returns 400 Bad Request with a collection of <see cref="Error"/> for validation or client errors.
        /// Returns 404 Not Found with a collection of <see cref="Error"/> when the cart or item is not found.
        /// Returns 409 Conflict with a collection of <see cref="Error"/> when a concurrency or business conflict occurs.
        /// Returns 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected server errors.
        /// </returns>
        [HttpPut("update-cart-item-quantity/{CartId},{ProductId},{Quantity}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCartItemQuantity(string CartId, string ProductId, short Quantity)
        {
            Result result = await sender.Send(new UpdateCartItemQuantityCommand(CartId, ProductId,Quantity));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieve the authenticated user's cart with paging.
        /// </summary>
        /// <param name="PageNumber">The page number to retrieve (1-based).</param>
        /// <param name="PageSize">The number of items per page.</param>
        /// <returns>
        /// Returns 200 OK with a <see cref="PagedResult{T}"/> wrapping a paged list of cart items when items are found.
        /// Returns 400 Bad Request with a collection of <see cref="Error"/> for invalid paging parameters or client errors.
        /// Returns 401 Unauthorized when the request does not contain a valid user token.
        /// Returns 404 Not Found with a collection of <see cref="Error"/> when the cart is not found for the user.
        /// Returns 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected server errors.
        /// </returns>
        [HttpGet("get-cart-by-user/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<GetCartByUserQueryresponse>), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
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
