using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.DiscountController
{
    /// <summary>
    /// API endpoints for managing discounts (create, read, update, delete and listing).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Discount")]
    [Authorize(Roles = "Admin")]

    public class DiscountController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Creates a new discount.
        /// </summary>
        /// <remarks>
        /// This action creates a new discount record in the system. It is used by administrators to define promotional discounts
        /// with specific types (percentage, fixed amount, or free shipping), values, and active date ranges.
        /// This endpoint requires Admin role authentication. Use this when you need to add a new discount that customers can apply
        /// to their orders during the specified active period.
        /// </remarks>
        /// <param name="model">The discount creation model (from request body) containing:
        /// - Name: The name or description of the discount.
        /// - DiscountType: The type of discount (Percentage, FixedAmount, or FreeShipping).
        /// - DiscountValue: The numeric value for the discount (e.g., 10 for 10% or 10.00 for fixed amount).
        /// - StartDate: The date when the discount becomes active.
        /// - EndDate: The date when the discount expires.
        /// - IsActive: A flag indicating whether the discount is currently active.</param>
        /// <returns>
        /// 201 Created with a <see cref="Result"/> when the discount is successfully created.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request is invalid (e.g., missing required fields, invalid types).
        /// 409 Conflict with a collection of <see cref="Error"/> when a duplicate or conflicting resource exists (e.g., duplicate discount name).
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPost("add-discount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDiscount([FromBody] AddDiscountModel model)
        {
            Result result = await sender.Send(new AddDiscountCommand(model.Name, model.DiscountType,
                                model.DiscountValue, model.StartDate, model.EndDate, model.IsActive));

            return result.ToActionResult();
        }

        /// <summary>
        /// Updates an existing discount.
        /// </summary>
        /// <remarks>
        /// This action modifies an existing discount record. It is used by administrators to adjust discount details such as
        /// values, active periods, or status. All properties of the discount can be updated including name, type, value, and active status.
        /// This endpoint requires Admin role authentication. Use this when you need to modify an active discount or extend/reduce its active period.
        /// </remarks>
        /// <param name="model">The discount update model (from request body) containing:
        /// - Id: The unique identifier of the discount to update.
        /// - Name: The updated name or description of the discount.
        /// - DiscountType: The updated type of discount (Percentage, FixedAmount, or FreeShipping).
        /// - DiscountValue: The updated numeric value for the discount.
        /// - StartDate: The updated date when the discount becomes active.
        /// - EndDate: The updated date when the discount expires.
        /// - IsActive: The updated flag indicating whether the discount is currently active.</param>
        /// <returns>
        /// 200 OK with a <see cref="Result"/> when the update succeeds.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request is invalid (e.g., invalid field values or malformed data).
        /// 404 Not Found with a collection of <see cref="Error"/> when the target discount does not exist.
        /// 409 Conflict with a collection of <see cref="Error"/> when there is a concurrency or duplicate conflict (e.g., duplicate name after update).
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPut("update-discount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscountModel model)
        {
            Result result = await sender.Send(new UpdateDiscountCommand(model.Id,model.Name, model.DiscountType,
                                 model.DiscountValue, model.StartDate, model.EndDate, model.IsActive));

            return result.ToActionResult();
        }

        /// <summary>
        /// Removes (deletes) a discount by identifier.
        /// </summary>
        /// <remarks>
        /// This action permanently removes a discount from the system. It is used by administrators to delete discounts that are no longer needed.
        /// This endpoint requires Admin role authentication. Use this when you need to completely remove a discount from the system.
        /// Once deleted, the discount cannot be recovered and existing orders referencing it may be affected depending on system design.
        /// </remarks>
        /// <param name="Id">The unique identifier of the discount to remove (from route). This parameter is extracted from the URL path.</param>
        /// <returns>
        /// 204 No Content when deletion succeeds.
        /// 404 Not Found with a collection of <see cref="Error"/> when the discount does not exist.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpDelete("remove-discount/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveDiscount(string Id)
        {
            Result result = await sender.Send(new RemoveDiscountCommand(Id));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieves a discount by its identifier.
        /// </summary>
        /// <remarks>
        /// This action fetches detailed information about a specific discount. It is used to retrieve complete discount data including
        /// name, type, value, active dates, and status. This endpoint requires Admin role authentication.
        /// Use this when you need to view all details of a particular discount for review or verification purposes.
        /// </remarks>
        /// <param name="Id">The unique identifier of the discount to retrieve (from route). This parameter is extracted from the URL path.</param>
        /// <returns>
        /// 200 OK with a <see cref="Result{DiscountResponse}"/> containing the discount details when the discount is found.
        /// 404 Not Found with a collection of <see cref="Error"/> when the discount does not exist.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request is invalid (e.g., malformed identifier).
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpGet("get-discount-by-id/{Id}")]
        [ProducesResponseType(typeof(DiscountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDiscountById(string Id)
        {
            Result<DiscountResponse> result = await sender.Send(new GetDiscountByIdQuery(Id));

            return result.ToActionResult();
        }

        /// <summary>
        /// Returns a paged list of discounts.
        /// </summary>
        /// <remarks>
        /// This action retrieves a paginated collection of all discounts in the system. It is used by administrators to browse
        /// and manage discounts with support for pagination to handle large result sets efficiently.
        /// This endpoint requires Admin role authentication. Use this when you need to view multiple discounts with pagination support
        /// for display in a UI list or for administrative review of all active and inactive discounts.
        /// </remarks>
        /// <param name="PageNumber">The page number (1-based, extracted from route). The first page is page 1. For example, PageNumber=2 returns the second page of results.</param>
        /// <param name="PageSize">The maximum number of items per page (extracted from route). This value determines how many discount records are returned in a single response.</param>
        /// <returns>
        /// 200 OK with a <see cref="Result{PagedResult{DiscountResponse}}"/> containing the paginated list of discounts when results are available.
        /// 204 No Content when there are no matching discounts (empty result set).
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request parameters are invalid (e.g., negative page number, invalid page size).
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpGet("get-all-discounts-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<DiscountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDiscountsPaged(int PageNumber, byte PageSize)
        {
            Result<PagedResult<DiscountResponse>> result = await sender.Send(new GetAllDiscountQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }


    }
}
