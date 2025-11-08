using Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;
using Löwen.Application.Features.OrderFeature.Commands.UpdateOrderSataus.UpdateOrderStatus;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.DeliveryController
{
    /// <summary>
    /// API endpoints for delivery personnel to manage and view assigned orders.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Delivery")]
    public class DeliveryController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Gets a paged list of orders assigned to the currently authenticated delivery user.
        /// </summary>
        /// <param name="PageNumber">The page number to retrieve (1-based).</param>
        /// <param name="PageSize">The number of items per page.</param>
        /// <returns>
        /// Returns a <see cref="Result{PagedResult{GetAssignedOrdersQueryresponse}}"/> with the paged assigned orders on success.
        /// Possible HTTP responses:
        /// - 200 OK: Paged results returned.
        /// - 400 Bad Request: Invalid request parameters.
        /// - 401 Unauthorized: Missing or invalid authentication token.
        /// - 404 Not Found: No assigned orders were found for the user.
        /// - 500 Internal Server Error: Unexpected server error.
        /// </returns>
        [HttpGet("get-assigned-orders/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result<PagedResult<GetAssignedOrdersQueryresponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignedOrders(int PageNumber, byte PageSize)
        {
            var Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(Id))
                return Result.Failure(new Error("api/Order/add-order", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result<PagedResult<GetAssignedOrdersQueryresponse>> result = await sender.Send(new GetAssignedOrdersQuery(Id,PageNumber, PageSize));

            return result.ToActionResult();
        }

        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        /// <param name="Id">The identifier of the order to update.</param>
        /// <param name="Status">The new <see cref="OrderStatus"/> value to set for the order.</param>
        /// <returns>
        /// Returns a <see cref="Result"/> indicating whether the update succeeded.
        /// Possible HTTP responses:
        /// - 200 OK: The order status was updated successfully.
        /// - 400 Bad Request: The request is invalid (e.g., malformed Id or invalid status).
        /// - 404 Not Found: The specified order was not found.
        /// - 409 Conflict: The update could not be applied due to a conflict (e.g., invalid state transition).
        /// - 500 Internal Server Error: Unexpected server error.
        /// </returns>
        [HttpPut("update-order-status/{Id},{Status}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrderStatuse(string Id, OrderStatus Status)
        {
            Result result = await sender.Send(new UpdateOrderStatusCommand(Id, Status));

            return result.ToActionResult();
        }


    }
}
