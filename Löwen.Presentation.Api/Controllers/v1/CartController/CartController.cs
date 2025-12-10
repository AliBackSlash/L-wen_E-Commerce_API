namespace Löwen.Presentation.Api.Controllers.v1.CartController
{
    /// <summary>
    /// API endpoints to manage the current user's shopping cart.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Cart")]
    [Authorize(Roles = "User")]
    public class CartController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Add an item to the authenticated user's cart.
        /// </summary>
        /// <remarks>
        /// This action allows an authenticated user to add a new product to their shopping cart or increase the quantity
        /// of an existing cart item. The product identifier and quantity are provided in the request body as a JSON payload.
        ///
        /// Use this endpoint when:
        /// - A user selects a product to purchase and wants to add it to their cart.
        /// - A user wants to increase the quantity of a product already in their cart.
        ///
        /// The authenticated user's identity is extracted from the bearer token in the Authorization header.
        /// All requests must include a valid JWT token for a user with the "User" role.
        /// </remarks>
        /// <param name="model">
        /// The request body containing the cart item details.
        /// This is a JSON object with two required properties:
        /// - ProductId (string): The unique identifier of the product to add to the cart.
        /// - Quantity (short): The number of units to add to the cart. Must be a positive integer.
        /// Retrieved from the request body via [FromBody] binding.
        /// </param>
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

            Result result = await sender.Send(new AddToCartCommand(id, model.ProductId, model.Quantity));

            return result.ToActionResult(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Remove an item from a cart.
        /// </summary>
        /// <remarks>
        /// This action removes a specific product from the authenticated user's shopping cart.
        /// Both the cart identifier and product identifier are provided as route parameters in the URL path.
        /// 
        /// Use this endpoint when:
        /// - A user wants to remove a product entirely from their cart.
        /// - A user decides not to purchase a previously added product.
        /// 
        /// The authenticated user's identity is required to ensure that users can only remove items from their own cart.
        /// A valid JWT token with the "User" role must be included in the Authorization header.
        /// </remarks>
        /// <param name="CartId">
        /// The unique identifier of the cart from which the item will be removed.
        /// Retrieved from the URL route parameter.
        /// Example URL: /api/Cart/remove-from-cart/{CartId},{ProductId}
        /// </param>
        /// <param name="ProductId">
        /// The unique identifier of the product to remove from the cart.
        /// Retrieved from the URL route parameter.
        /// Example URL: /api/Cart/remove-from-cart/{CartId},{ProductId}
        /// </param>
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

            return result.ToActionResult(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Update the quantity of an existing cart item.
        /// </summary>
        /// <remarks>
        /// This action modifies the quantity of a specific product already present in the authenticated user's shopping cart.
        /// The cart identifier, product identifier, and new quantity are all provided as route parameters in the URL path.
        /// 
        /// Use this endpoint when:
        /// - A user wants to change the quantity of a product in their cart (increase or decrease).
        /// - A user wishes to adjust the number of units for an item before proceeding to checkout.
        /// 
        /// The authenticated user must have a valid JWT token with the "User" role in the Authorization header.
        /// The quantity update is subject to business validation rules and inventory constraints.
        /// </remarks>
        /// <param name="CartId">
        /// The unique identifier of the cart containing the item to update.
        /// Retrieved from the URL route parameter.
        /// Example URL: /api/Cart/update-cart-item-quantity/{CartId},{ProductId},{Quantity}
        /// </param>
        /// <param name="ProductId">
        /// The unique identifier of the product whose quantity will be updated.
        /// Retrieved from the URL route parameter.
        /// Example URL: /api/Cart/update-cart-item-quantity/{CartId},{ProductId},{Quantity}
        /// </param>
        /// <param name="Quantity">
        /// The new quantity for the specified cart item. Must be a positive integer represented as a short (16-bit integer).
        /// Retrieved from the URL route parameter.
        /// Example URL: /api/Cart/update-cart-item-quantity/{CartId},{ProductId},{Quantity}
        /// </param>
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
            Result result = await sender.Send(new UpdateCartItemQuantityCommand(CartId, ProductId, Quantity));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieve the authenticated user's cart with paging.
        /// </summary>
        /// <remarks>
        /// This action retrieves all items in the authenticated user's shopping cart with support for pagination.
        /// The results are returned as a paged collection, allowing clients to fetch cart items in smaller batches
        /// rather than retrieving the entire cart at once.
        /// 
        /// Use this endpoint when:
        /// - A user needs to view the contents of their shopping cart.
        /// - A client application requires paginated cart data for display or processing purposes.
        /// - A user wants to review their selected items before proceeding to checkout.
        /// 
        /// The page number and page size are provided as route parameters in the URL path.
        /// The authenticated user's identity is extracted from the bearer token in the Authorization header.
        /// All requests must include a valid JWT token for a user with the "User" role.
        /// </remarks>
        /// <param name="PageNumber">
        /// The page number to retrieve in a 1-based indexed pagination scheme.
        /// Use 1 to retrieve the first page of results.
        /// Retrieved from the URL route parameter.
        /// Example URL: /api/Cart/get-cart-by-user/{PageNumber},{PageSize}
        /// </param>
        /// <param name="PageSize">
        /// The maximum number of cart items to include in a single page of results.
        /// Must be a positive integer represented as a byte (8-bit unsigned integer), limiting the maximum page size to 255 items.
        /// Retrieved from the URL route parameter.
        /// Example URL: /api/Cart/get-cart-by-user/{PageNumber},{PageSize}
        /// </param>
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
