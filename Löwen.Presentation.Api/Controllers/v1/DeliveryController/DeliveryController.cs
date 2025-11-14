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
    [Authorize(Roles = "Delivery")]

    public class DeliveryController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Gets a paged list of orders assigned to the currently authenticated delivery user.
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves a paginated list of all orders assigned to the delivery personnel making the request.
        /// The orders are filtered based on the user's identity extracted from the authentication token.
        /// This action is restricted to users with the "Delivery" role and requires a valid authentication token.
        /// 
        /// Use this endpoint to display the list of orders that a delivery person is responsible for, with support
        /// for pagination to handle large datasets efficiently.
        /// </remarks>
        /// <param name="PageNumber">
        /// The page number to retrieve (1-based indexing). This route parameter specifies which page of results to return.
        /// For example, page 1 returns the first set of results, page 2 returns the second set, and so on.
        /// Retrieved from the URL route.
        /// </param>
        /// <param name="PageSize">
        /// The number of items per page. This route parameter controls how many orders are returned in each page.
        /// Retrieved from the URL route.
        /// </param>
        /// <returns>
        /// Returns a <see cref="PagedResult{GetAssignedOrdersQueryresponse}"/> containing a paged collection of assigned orders on success.
        /// Each result includes order details relevant to delivery personnel.
        /// 
        /// Possible HTTP responses:
        /// - 200 OK: Paged results returned successfully with assigned orders.
        /// - 400 Bad Request: Invalid request parameters (e.g., invalid page number or page size values).
        /// - 401 Unauthorized: Missing or invalid authentication token, or user is not authenticated.
        /// - 404 Not Found: No assigned orders were found for the authenticated user.
        /// - 500 Internal Server Error: Unexpected server error occurred during processing.
        /// </returns>
        [HttpGet("get-assigned-orders/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<GetAssignedOrdersQueryresponse>), StatusCodes.Status200OK)]
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
        /// <remarks>
        /// This endpoint allows delivery personnel to update the status of an order they are responsible for.
        /// The status change must be a valid transition according to the order lifecycle rules.
        /// This action is restricted to users with the "Delivery" role and requires a valid authentication token.
        /// 
        /// Use this endpoint to mark orders as in-progress, delivered, or apply other valid status transitions
        /// as the delivery process progresses. The system will validate that the status transition is legal
        /// before applying the change.
        /// </remarks>
        /// <param name="Id">
        /// The unique identifier of the order to update. This route parameter specifies which order's status will be changed.
        /// Retrieved from the URL route.
        /// </param>
        /// <param name="Status">
        /// The new <see cref="OrderStatus"/> value to set for the order. This route parameter specifies the target status.
        /// Must be a valid <see cref="OrderStatus"/> enumeration value.
        /// Retrieved from the URL route.
        /// </param>
        /// <returns>
        /// Returns a result indicating whether the status update was successful.
        /// On success, the order's status is updated and persisted in the system.
        /// 
        /// Possible HTTP responses:
        /// - 200 OK: The order status was updated successfully.
        /// - 400 Bad Request: The request is invalid (e.g., malformed order Id or invalid status enumeration value).
        /// - 404 Not Found: The specified order was not found in the system, or the order is not assigned to the authenticated user.
        /// - 409 Conflict: The status update could not be applied due to a conflict (e.g., invalid state transition, order already in a terminal state).
        /// - 500 Internal Server Error: Unexpected server error occurred during the update operation.
        /// </returns>
        [HttpPut("update-order-status/{Id},{Status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
