namespace Löwen.Presentation.Api.Controllers.v1.CouponController
{
    /// <summary>
    /// API endpoints for managing coupons and applying/removing coupons on orders.
    /// Provides create, read, update and delete operations as well as order-related actions.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Coupon")]
    public class CouponController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Create a new coupon.
        /// </summary>
        /// <param name="model">Coupon creation model containing code, discount, validity and usage limit.</param>
        /// <returns>
        /// 201 Created with the operation <see cref="Result"/> when creation succeeds.
        /// 400 Bad Request when the model is invalid.
        /// 409 Conflict when a coupon with the same identity (e.g. code) already exists.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("add-coupon")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCoupon([FromBody] AddCouponModel model)
        {
            Result result = await sender.Send(new AddCouponCommand(model.Code,model.DiscountType,
                                                 model.DiscountValue, model.StartDate, model.EndDate, model.IsActive, model.UsageLimit));

            return result.ToActionResult();
        }

        /// <summary>
        /// Update an existing coupon.
        /// </summary>
        /// <param name="model">Coupon update model containing the coupon id and updated fields.</param>
        /// <returns>
        /// 200 OK with the operation <see cref="Result"/> when update succeeds.
        /// 400 Bad Request when the model is invalid.
        /// 404 Not Found when the coupon to update does not exist.
        /// 409 Conflict when the update cannot be applied due to a concurrency or duplicate constraint.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("update-coupon")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCoupon([FromBody] UpdateCouponModel model)
        {
            Result result = await sender.Send(new UpdateCouponCommand(model.CouponId,model.Code, model.DiscountType,model.DiscountValue,
                model.StartDate, model.EndDate, model.IsActive, model.UsageLimit));

            return result.ToActionResult();
        }

        /// <summary>
        /// Remove a coupon by identifier.
        /// </summary>
        /// <param name="Id">Identifier of the coupon to remove.</param>
        /// <returns>
        /// 204 No Content when the coupon was successfully deleted.
        /// 404 Not Found when no coupon with the provided id exists.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpDelete("remove-coupon/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveCoupon(string Id)
        {
            Result result = await sender.Send(new RemoveCouponCommand(Id));

            return result.ToActionResult();
        }


        /// <summary>
        /// Apply a coupon to an order.
        /// </summary>
        /// <param name="CouponCode">Coupon code to apply.</param>
        /// <param name="OrderId">Identifier of the order to which the coupon will be applied.</param>
        /// <returns>
        /// 201 Created with the operation <see cref="Result"/> when the coupon is applied successfully.
        /// 400 Bad Request when input is invalid.
        /// 409 Conflict when the coupon cannot be applied (e.g., already applied, usage limits reached).
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("apply-coupon-to-order/{CouponCode},{OrderId}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApplyCouponToOrder(string CouponCode,string OrderId)
        {
            Result result = await sender.Send(new ApplyCouponToOrderCommand(CouponCode, OrderId));

            return result.ToActionResult();
        }

        /// <summary>
        /// Remove a coupon from an order.
        /// </summary>
        /// <param name="CouponCode">Coupon code to remove from the order.</param>
        /// <param name="OrderId">Identifier of the order.</param>
        /// <returns>
        /// 204 No Content when the coupon was removed from the order.
        /// 404 Not Found when the coupon or order (or their relationship) cannot be found.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpDelete("remove-coupon-from-order/{CouponCode},{OrderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveCouponFromOrder(string CouponCode, string OrderId)
        {
            Result result = await sender.Send(new RemoveCouponFromOrderCommand(CouponCode, OrderId));

            return result.ToActionResult();
        }


        /// <summary>
        /// Retrieve a coupon by its identifier.
        /// </summary>
        /// <param name="Id">Identifier of the coupon to retrieve.</param>
        /// <returns>
        /// 200 OK with the <see cref="Result{GetCouponQueryResponse}"/> when the coupon is found.
        /// 400 Bad Request when the provided id is invalid.
        /// 404 Not Found when the coupon does not exist.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-coupon-by-id/{Id}")]
        [ProducesResponseType(typeof(Result<GetCouponQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCouponById(string Id)
        {
            Result<GetCouponQueryResponse> result = await sender.Send(new GetCouponByIdQuery(Id));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieve a coupon by its code.
        /// </summary>
        /// <param name="Code">Code of the coupon to retrieve.</param>
        /// <returns>
        /// 200 OK with the <see cref="Result{GetCouponQueryResponse}"/> when the coupon is found.
        /// 400 Bad Request when the provided code is invalid.
        /// 404 Not Found when the coupon does not exist.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-coupon-by-code/{Code}")]
        [ProducesResponseType(typeof(Result<GetCouponQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCouponByCode(string Code)
        {
            Result<GetCouponQueryResponse> result = await sender.Send(new GetCouponByCodeQuery(Code));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieve a paged list of coupons.
        /// </summary>
        /// <param name="PageNumber">Page number to retrieve (1-based).</param>
        /// <param name="PageSize">Size of each page.</param>
        /// <returns>
        /// 200 OK with a <see cref="Result{PagedResult{GetCouponQueryResponse}}"/> when there are results.
        /// 204 No Content when the requested page has no coupons.
        /// 400 Bad Request when paging parameters are invalid.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-all-coupons-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result<PagedResult<GetCouponQueryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCouponsPaged(int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetCouponQueryResponse>> result = await sender.Send(new GetAllCouponsQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }

    }
}
