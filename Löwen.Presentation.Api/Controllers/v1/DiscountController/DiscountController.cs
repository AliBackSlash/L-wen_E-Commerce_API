using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.DiscountController
{
    /// <summary>
    /// API endpoints for managing discounts (create, read, update, delete and listing).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Discount")]
    public class DiscountController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Creates a new discount.
        /// </summary>
        /// <param name="model">The discount creation model containing name, type, value and active period.</param>
        /// <returns>
        /// 201 Created with a <see cref="Result"/> when the discount is successfully created.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request is invalid.
        /// 409 Conflict with a collection of <see cref="Error"/> when a duplicate or conflicting resource exists.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPost("add-discount")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
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
        /// <param name="model">The discount update model containing id and new values.</param>
        /// <returns>
        /// 200 OK with a <see cref="Result"/> when the update succeeds.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request is invalid.
        /// 404 Not Found with a collection of <see cref="Error"/> when the target discount does not exist.
        /// 409 Conflict with a collection of <see cref="Error"/> when there is a concurrency or duplicate conflict.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPut("update-discount")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
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
        /// <param name="Id">The identifier of the discount to remove.</param>
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
        /// <param name="Id">The identifier of the discount to retrieve.</param>
        /// <returns>
        /// 200 OK with a <see cref="Result{DiscountResponse}"/> when the discount is found.
        /// 404 Not Found with a collection of <see cref="Error"/> when the discount does not exist.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request is invalid.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpGet("get-discount-by-id/{Id}")]
        [ProducesResponseType(typeof(Result<DiscountResponse>), StatusCodes.Status200OK)]
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
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Page size (number of items per page).</param>
        /// <returns>
        /// 200 OK with a <see cref="Result{PagedResult{DiscountResponse}}"/> when results are available.
        /// 204 No Content when there are no matching discounts.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request parameters are invalid.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpGet("get-all-discounts-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result<PagedResult<DiscountResponse>>), StatusCodes.Status200OK)]
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
