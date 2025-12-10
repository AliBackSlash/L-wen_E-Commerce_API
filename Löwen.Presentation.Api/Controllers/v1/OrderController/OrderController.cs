namespace Löwen.Presentation.Api.Controllers.v1.OrderController
{
    /// <summary>
    /// Exposes Order-related HTTP endpoints (v1).
    /// Contains actions to create, update and read orders and order items.
    /// All endpoints return a Result wrapper and use standard HTTP status codes to describe outcome.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Order")]
    [Authorize(Roles = "User")]
    public class OrderController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Create a new order for the authenticated customer.
        /// </summary>
        /// <remarks>
        /// This action allows authenticated users to submit a new order containing one or more order items.
        /// The order is created with the current authenticated user's identifier extracted from the security context.
        /// This operation requires the user to be authenticated and to have the "User" role.
        /// Use this endpoint when a customer has selected products and is ready to place an order for purchase.
        /// </remarks>
        /// <param name="model">The order request model passed in the HTTP request body containing a collection of order items to be added to the order. 
        /// Each item specifies the product and quantity to be ordered.</param>
        /// <returns>
        /// 201 Created with a Result object on successful order creation.
        /// 400 Bad Request when the request body is invalid, malformed, or contains invalid order items.
        /// 409 Conflict when a duplicate resource or business rule conflict exists (e.g., inventory constraints).
        /// 500 Internal Server Error on unexpected server-side failures.
        /// </returns>
        [HttpPost("add-order")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrder([FromBody] OrderItemModel model)
        {
            var CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(CustomerId))
                return Result.Failure(new Error("api/Order/add-order", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result result = await sender.Send(new AddOrderCommand(CustomerId, model.orders));
            return result.ToActionResult(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Update existing order items with modified quantity, price, or delivery information.
        /// </summary>
        /// <remarks>
        /// This action enables modification of order item details for an existing order.
        /// Updates can include changes to quantity ordered, price adjustments, or delivery options.
        /// This operation requires the user to be authenticated and to have the "User" role.
        /// Use this endpoint when a customer needs to adjust items in a previously created order before final checkout or fulfillment.
        /// </remarks>
        /// <param name="model">The update request model passed in the HTTP request body containing the order identifier, 
        /// delivery identifier, product identifier, and the new quantity and price values to apply.</param>
        /// <returns>
        /// 200 OK with a Result object when the order item is successfully updated.
        /// 400 Bad Request when the request body is invalid, malformed, or contains invalid update values.
        /// 404 Not Found when the specified order or order item cannot be found in the system.
        /// 409 Conflict when the update conflicts with business rules (e.g., insufficient inventory, duplicate state, or pricing constraints).
        /// 500 Internal Server Error on unexpected server-side failures.
        /// </returns>
        [HttpPut("update-order-items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderItemModel model)
        {
            Result result = await sender.Send(new UpdateOrderItemCommand(model.OrderId, model.deliveryId, model.ProductId, model.Quantity, model.Price));

            return result.ToActionResult();
        }


        /// <summary>
        /// Retrieve detailed information for a single order by its unique identifier.
        /// </summary>
        /// <remarks>
        /// This action fetches comprehensive details about a specific order, including all associated order items and their information.
        /// The order identifier is provided as a route parameter in the URL path.
        /// This operation requires the user to be authenticated and to have the "User" role.
        /// Use this endpoint when you need to view complete information about a specific order, such as during order review or tracking.
        /// </remarks>
        /// <param name="Id">The unique order identifier to retrieve, passed as a route parameter in the URL path (e.g., /api/Order/get-order-details/{Id}).</param>
        /// <returns>
        /// 200 OK with a Result containing <see cref="GetOrderDetailsQueryResponse"/> object when the order is found and retrieved successfully.
        /// 400 Bad Request when the request is malformed or the provided identifier format is invalid.
        /// 404 Not Found when no order with the specified identifier exists in the system.
        /// 500 Internal Server Error on unexpected server-side failures.
        /// </returns>
        [HttpGet("get-order-details/{Id}")]
        [ProducesResponseType(typeof(GetOrderDetailsQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderDetails(string Id)
        {
            Result<GetOrderDetailsQueryResponse> result = await sender.Send(new GetOrderDetailsQuery(Id));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieve a paginated list of orders belonging to the authenticated user.
        /// </summary>
        /// <remarks>
        /// This action returns order data specific to the authenticated user, supporting pagination to handle large result sets.
        /// The authenticated user's identifier is extracted from the security context automatically.
        /// This operation requires the user to be authenticated and to have the "User" role.
        /// Use this endpoint when displaying a customer's order history or when the customer needs to browse their orders across multiple pages.
        /// Pagination allows efficient retrieval of results without overwhelming the client or server with large datasets.
        /// </remarks>
        /// <param name="PageNumber">The page number to retrieve in the paginated result set, passed as a route parameter (1-based indexing). 
        /// Must be a positive integer greater than or equal to 1.</param>
        /// <param name="PageSize">The number of order items to include per page, passed as a route parameter. Must be a positive byte value (1-255).</param>
        /// <returns>
        /// 200 OK with a Result containing a <see cref="PagedResult{T}"/> of <see cref="GetOrderDetailsQueryResponse"/> when one or more orders are returned.
        /// 204 No Content when the query returns no orders for the authenticated user (either no orders exist or the page is beyond the available data).
        /// 400 Bad Request when paging parameters are invalid or out of acceptable range.
        /// 500 Internal Server Error on unexpected server-side failures.
        /// </returns>
        [HttpGet("get-orders-by-user-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<GetOrderDetailsQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrdersByUser(int PageNumber, byte PageSize)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(id))
                return Result.Failure(new Error("api/Order/get-orders-by-user", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result<PagedResult<GetOrderDetailsQueryResponse>> result = await sender.Send(new GetOrdersForUserQuery(id, PageNumber, PageSize));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieve a paginated list of all orders in the system.
        /// </summary>
        /// <remarks>
        /// This action returns all orders from the system across all customers, supporting pagination for manageable result sets.
        /// This operation requires the user to be authenticated and to have the "Admin" role, restricting access to authorized administrators only.
        /// Use this endpoint for administrative purposes such as order monitoring, reporting, system auditing, or full order management across all customers.
        /// Pagination allows efficient retrieval of large datasets without performance degradation.
        /// </remarks>
        /// <param name="PageNumber">The page number to retrieve in the paginated result set, passed as a route parameter (1-based indexing). 
        /// Must be a positive integer greater than or equal to 1.</param>
        /// <param name="PageSize">The number of order items to include per page, passed as a route parameter. Must be a positive byte value (1-255).</param>
        /// <returns>
        /// 200 OK with a Result containing a <see cref="PagedResult{T}"/> of <see cref="GetOrderDetailsQueryResponse"/> when one or more orders are returned.
        /// 204 No Content when the query returns no orders in the system or the page is beyond the available data.
        /// 400 Bad Request when paging parameters are invalid or out of acceptable range.
        /// 500 Internal Server Error on unexpected server-side failures.
        /// </returns>
        [HttpGet("get-orders-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<GetOrderDetailsQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetOrdersPaged(int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetOrderDetailsQueryResponse>> result = await sender.Send(new GetAllOrdersQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }

    }
}
