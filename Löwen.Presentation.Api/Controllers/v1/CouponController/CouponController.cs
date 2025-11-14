namespace Löwen.Presentation.Api.Controllers.v1.CouponController
{
    /// <summary>
    /// API endpoints for managing coupons and applying/removing coupons on orders.
    /// Provides create, read, update and delete operations as well as order-related actions.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Coupon")]
    [Authorize( Roles = "Admin")]
    public class CouponController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Create a new coupon.
        /// </summary>
        /// <remarks>
        /// Creates a new coupon with specified discount details and validity period. This action allows administrators to define promotional coupons 
        /// that can be applied to customer orders. The coupon can have percentage-based, fixed amount, or free shipping discounts. 
        /// Use this endpoint when you need to introduce new promotional offers to the system.
        /// 
        /// **Authorization**: Requires Admin role.
        /// </remarks>
        /// <param name="model">
        /// Coupon creation model containing code, discount type, discount value, validity dates, active status, and usage limit.
        /// This parameter is retrieved from the request body in JSON format.
        /// </param>
        /// <returns>
        /// 201 Created with the operation when creation succeeds.
        /// 400 Bad Request when the model is invalid.
        /// 409 Conflict when a coupon with the same identity (e.g. code) already exists.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("add-coupon")]
        [ProducesResponseType(StatusCodes.Status201Created)]
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
        /// <remarks>
        /// Modifies an existing coupon's attributes such as code, discount type, discount value, validity period, active status, and usage limit.
        /// This action enables administrators to adjust promotional details after creation, including changing discount parameters or extending/shortening 
        /// the validity period. Use this endpoint to fine-tune active or inactive coupons.
        /// 
        /// **Authorization**: Requires Admin role.
        /// </remarks>
        /// <param name="model">
        /// Coupon update model containing the coupon identifier and all updated fields (code, discount type, discount value, dates, active status, usage limit).
        /// This parameter is retrieved from the request body in JSON format.
        /// </param>
        /// <returns>
        /// 200 OK with the operation when update succeeds.
        /// 400 Bad Request when the model is invalid.
        /// 404 Not Found when the coupon to update does not exist.
        /// 409 Conflict when the update cannot be applied due to a concurrency or duplicate constraint.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("update-coupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <remarks>
        /// Permanently deletes a coupon from the system using its unique identifier. This action removes the coupon completely and prevents 
        /// any further use or application. Use this endpoint when a coupon is no longer needed or should be withdrawn from the promotional offering. 
        /// Existing coupon records associated with orders remain in the database for historical purposes.
        /// 
        /// **Authorization**: Requires Admin role.
        /// </remarks>
        /// <param name="Id">
        /// Unique identifier of the coupon to remove. This parameter is retrieved from the URL route path.
        /// </param>
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
        /// <remarks>
        /// Applies an existing coupon to a specific order, granting the customer the associated discount. This action verifies coupon validity 
        /// (active status, date range, usage limits) before applying it to the order. The discount is calculated and reflected in the order total. 
        /// Use this endpoint when a customer wants to redeem a promotional code for their purchase.
        /// 
        /// **Authorization**: Requires User role.
        /// </remarks>
        /// <param name="CouponCode">
        /// The promotional code of the coupon to apply. This parameter is retrieved from the URL route path.
        /// </param>
        /// <param name="OrderId">
        /// Unique identifier of the order to which the coupon will be applied. This parameter is retrieved from the URL route path.
        /// </param>
        /// <returns>
        /// 201 Created with the operation when the coupon is applied successfully.
        /// 400 Bad Request when input is invalid.
        /// 409 Conflict when the coupon cannot be applied (e.g., already applied, usage limits reached, coupon expired, coupon not active).
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("apply-coupon-to-order/{CouponCode},{OrderId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> ApplyCouponToOrder(string CouponCode,string OrderId)
        {
            Result result = await sender.Send(new ApplyCouponToOrderCommand(CouponCode, OrderId));

            return result.ToActionResult();
        }

        /// <summary>
        /// Remove a coupon from an order.
        /// </summary>
        /// <remarks>
        /// Detaches a previously applied coupon from an order, removing its discount from the order total. This action allows customers to cancel 
        /// coupon applications if desired. Use this endpoint when a customer wishes to remove a promotional code they've already applied to their order, 
        /// returning the order to its full price.
        /// 
        /// **Authorization**: Requires User role.
        /// </remarks>
        /// <param name="CouponCode">
        /// The promotional code of the coupon to remove from the order. This parameter is retrieved from the URL route path.
        /// </param>
        /// <param name="OrderId">
        /// Unique identifier of the order from which the coupon will be removed. This parameter is retrieved from the URL route path.
        /// </param>
        /// <returns>
        /// 204 No Content when the coupon was removed from the order.
        /// 404 Not Found when the coupon or order (or their relationship) cannot be found.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpDelete("remove-coupon-from-order/{CouponCode},{OrderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> RemoveCouponFromOrder(string CouponCode, string OrderId)
        {
            Result result = await sender.Send(new RemoveCouponFromOrderCommand(CouponCode, OrderId));

            return result.ToActionResult();
        }


        /// <summary>
        /// Retrieve a coupon by its identifier.
        /// </summary>
        /// <remarks>
        /// Fetches the complete details of a specific coupon using its unique identifier. This action returns comprehensive coupon information 
        /// including code, discount type, discount value, validity period, active status, and usage statistics. Use this endpoint to inspect 
        /// coupon properties for administrative purposes or to verify coupon details before applying it to an order.
        /// 
        /// **Authorization**: Requires Admin role.
        /// </remarks>
        /// <param name="Id">
        /// Unique identifier of the coupon to retrieve. This parameter is retrieved from the URL route path.
        /// </param>
        /// <returns>
        /// 200 OK with the <see cref="GetCouponQueryResponse"/> when the coupon is found.
        /// 400 Bad Request when the provided id is invalid.
        /// 404 Not Found when the coupon does not exist.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-coupon-by-id/{Id}")]
        [ProducesResponseType(typeof(GetCouponQueryResponse), StatusCodes.Status200OK)]
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
        /// <remarks>
        /// Fetches the complete details of a coupon using its promotional code. This action is commonly used by customers to validate a coupon 
        /// code before applying it to their order, or by administrators to look up coupon details by code. The endpoint returns comprehensive 
        /// coupon information including discount type, value, validity dates, and active status. Use this endpoint when you need to find a coupon 
        /// by its customer-facing promotional code.
        /// 
        /// **Authorization**: Requires Admin role.
        /// </remarks>
        /// <param name="Code">
        /// The promotional code of the coupon to retrieve. This parameter is retrieved from the URL route path.
        /// </param>
        /// <returns>
        /// 200 OK with the <see cref="GetCouponQueryResponse"/> when the coupon is found.
        /// 400 Bad Request when the provided code is invalid.
        /// 404 Not Found when the coupon does not exist.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-coupon-by-code/{Code}")]
        [ProducesResponseType(typeof(GetCouponQueryResponse), StatusCodes.Status200OK)]
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
        /// <remarks>
        /// Fetches a paginated list of all coupons in the system, allowing administrators to browse, manage, and oversee the complete coupon catalog. 
        /// Results are organized into pages for efficient data handling. Use this endpoint to view all available coupons with pagination, 
        /// making it easier to manage large sets of promotional codes without retrieving the entire dataset at once.
        /// 
        /// **Authorization**: Requires Admin role.
        /// </remarks>
        /// <param name="PageNumber">
        /// The page number to retrieve (1-based indexing). This parameter is retrieved from the URL route path.
        /// </param>
        /// <param name="PageSize">
        /// The number of coupons to return per page. This parameter is retrieved from the URL route path.
        /// </param>
        /// <returns>
        /// 200 OK with a <see cref="PagedResult{GetCouponQueryResponse}"/> when there are results.
        /// 204 No Content when the requested page has no coupons.
        /// 400 Bad Request when paging parameters are invalid.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-all-coupons-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<GetCouponQueryResponse>), StatusCodes.Status200OK)]
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
