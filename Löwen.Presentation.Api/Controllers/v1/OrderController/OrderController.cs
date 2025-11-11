
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
        /// <param name="model">Order model containing order items to create.</param>
        /// <returns>
        /// 201 Created with a Result on success.
        /// 400 Bad Request when the request body is invalid.
        /// 409 Conflict when a duplicate resource or business conflict exists.
        /// 500 Internal Server Error on unexpected failures.
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
            return result.ToActionResult();
        }

        /// <summary>
        /// Update existing order items (quantity/price/delivery).
        /// </summary>
        /// <param name="model">Model containing identifiers and updated values for the order item.</param>
        /// <returns>
        /// 200 OK with a Result when update succeeds.
        /// 400 Bad Request when the request body or values are invalid.
        /// 404 Not Found when the targeted order or order item cannot be found.
        /// 409 Conflict when the update conflicts with business rules (e.g., inventory/duplicate state).
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpPut("update-order-items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderItemModel model)
        {
            Result result = await sender.Send(new UpdateOrderItemCommand(model.OrderId,model.deliveryId, model.ProductId, model.Quantity, model.Price));

            return result.ToActionResult();
        }


        /// <summary>
        /// Retrieve details for a single order by identifier.
        /// </summary>
        /// <param name="Id">Order identifier to look up.</param>
        /// <returns>
        /// 200 OK with a Result containing <see cref="GetOrderDetailsQueryResponse"/> when the order is found.
        /// 400 Bad Request when the request is malformed.
        /// 404 Not Found when the order with the specified Id does not exist.
        /// 500 Internal Server Error on unexpected failures.
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
        /// Retrieve a paged list of User orders.
        /// </summary>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Number of items per page.</param>
        /// <returns>
        /// 200 OK with a Result containing a paged result when one or more orders are returned.
        /// 204 No Content when the query returns no orders.
        /// 400 Bad Request when paging parameters are invalid.
        /// 500 Internal Server Error on unexpected failures.
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
        /// Retrieve a paged list of orders.
        /// </summary>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Number of items per page.</param>
        /// <returns>
        /// 200 OK with a Result containing a paged result when one or more orders are returned.
        /// 204 No Content when the query returns no orders.
        /// 400 Bad Request when paging parameters are invalid.
        /// 500 Internal Server Error on unexpected failures.
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
