namespace Löwen.Presentation.Api.Controllers.v1.UsersController;
[ApiController]
[ApiVersion("1.0")]
[Route("api/users")]
//[Authorize(Roles = "User")]
/// <summary>
/// Controller that exposes user-related endpoints (profile, password, images, wishlist, loves, reviews, orders).
/// All actions return application results wrapped in the project's Result/Result{T} pattern and follow REST semantics
/// for read, update, create and delete intents.
/// </summary>
/// <param name="_sender">MediatR sender used to dispatch commands and queries.</param>
/// <param name="fileService">File helper service used to upload and delete user images.</param>
public class UsersController(ISender _sender, IFileService fileService) : ControllerBase
{
    /// <summary>
    /// Retrieves the current user's information based on the authenticated token.
    /// </summary>
    /// <returns>
    /// 200 OK with the user details wrapped in a Result when the user is found;
    /// 400 Bad Request for invalid input;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 404 Not Found when the user does not exist;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpGet("get-user-info")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType<GetUserByIdQueryResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserInfo()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))      
            return Result.Failure(new Error("api/users/get-user-info", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();
        

        Result<GetUserByIdQueryResponse> result = await _sender.Send(new GetUserByIdQuery(id));

        return result.ToActionResult();
    }

    /// <summary>
    /// Updates the authenticated user's profile information.
    /// </summary>
    /// <param name="request">Profile fields to update.</param>
    /// <returns>
    /// 200 OK with update response when update succeeds;
    /// 400 Bad Request for invalid payloads;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 404 Not Found when the target user cannot be found;
    /// 409 Conflict when a unique constraint or concurrency conflict occurs;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPut("update-user-info")]
    [ProducesResponseType<UpdateUserInfoCommandResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserModel request)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))        
            return Result.Failure(new Error("api/users/update", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();


        Result<UpdateUserInfoCommandResponse> result = await _sender.Send(new UpdateUserInfoCommand
            (id, request.FName, request.MName, request.LName, request.DateOfBirth, request.PhoneNumber, request.Gender, request.AddressDetails));

        return result.ToActionResult();
    }



    /// <summary>
    /// Changes the authenticated user's password.
    /// </summary>
    /// <param name="request">Current and new password values.</param>
    /// <returns>
    /// 200 OK with change result when the password is successfully changed;
    /// 400 Bad Request for validation errors (e.g. mismatched confirm password);
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 409 Conflict when current password is incorrect or policy conflicts occur;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPut("change-password")]
    [ProducesResponseType<ChangePasswordCommandResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordModel request)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/change-password", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();


        Result<ChangePasswordCommandResponse> result = await _sender.Send(new ChangePasswordCommand(id, request.CurrentPassword,
            request.Password, request.ConfermPassword));

        return result.ToActionResult();
    }

    /// <summary>
    /// Updates the authenticated user's profile image.
    /// </summary>
    /// <param name="Image">Image file to upload.</param>
    /// <returns>
    /// 200 OK when the profile image update completes successfully;
    /// 400 Bad Request for invalid file uploads;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 409 Conflict when the uploaded file causes a conflict;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPut("update-profile-image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProfileImage(IFormFile Image)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/update-profile-image", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        var UploadResult = await fileService.UploadProfileImageAsync(Image);

        if (UploadResult is null)
            return Result.Failure(new Error("api/users/update-profile-image", "Profile image not uploaded",ErrorType.BadRequest)).ToActionResult();

        if (UploadResult.IsFailure)
            return UploadResult.ToActionResult();

        Result result = await _sender.Send(new UpdateProfileImageCommand(id, UploadResult.Value.ImageName, UploadResult.Value.CurrentRootPath));

        if (result.IsFailure)
            fileService.DeleteFile(UploadResult.Value.ImageName,false);

        return result.ToActionResult();

    }


    /// <summary>
    /// Removes the authenticated user's profile image.
    /// </summary>
    /// <returns>
    /// 204 No Content when deletion succeeds;
    /// 400 Bad Request for invalid requests;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 404 Not Found when the image or user does not exist;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPut("remove-user-image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveUserImage()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/update-profile-image", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result<string> result = await _sender.Send(new DeleteUserImageCommand(id));

        if (result.IsSuccess)
            fileService.DeleteFile(result.Value, false);

        return result.ToActionResult();
    }
    /// <summary>
    /// Sends an email confirmation token for the supplied email address (verify own email).
    /// </summary>
    /// <param name="email">Email address to verify.</param>
    /// <returns>
    /// 201 Created when the token was generated and sent;
    /// 400 Bad Request for invalid input;
    /// 409 Conflict when the email is already verified or a duplicate issue occurs;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPost("verify-own-email/{email}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VerifyOwnEmail(string email)
    {
        Result result = await _sender.Send(new EmailConfirmationTokenCommand(email));

        return result.ToActionResult();
    }

    //[HttpPost("verify-phone-number")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> VerifyPhoneNumber()
    //{
    //    throw new NotImplementedException();
    //}

    /// <summary>
    /// Adds a product to the authenticated user's wishlist.
    /// </summary>
    /// <param name="productId">Product identifier to add to wishlist.</param>
    /// <returns>
    /// 201 Created when the product is successfully added;
    /// 400 Bad Request for invalid requests;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 409 Conflict if the product is already in the wishlist;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPost("add-product-to-wishlist/{productId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddProductToWishlist(string productId)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/add-product-to-wishlist", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new AddToWishlistCommand(id,productId));

        return result.ToActionResult();
    }

    /// <summary>
    /// Removes a product from the authenticated user's wishlist.
    /// </summary>
    /// <param name="productId">Product identifier to remove.</param>
    /// <returns>
    /// 204 No Content when removal succeeds;
    /// 400 Bad Request for invalid requests;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 404 Not Found when the product is not present in the wishlist;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpDelete("remove-product-from-wishlist/{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveProductFromWishlist(string productId)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/remove-product-from-wishlist", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new RemoveFromWishlistCommand(id, productId));

        return result.ToActionResult();
    }

    /// <summary>
    /// Adds a 'love' (like) for a product on behalf of the authenticated user.
    /// </summary>
    /// <param name="productId">Product identifier to love.</param>
    /// <returns>
    /// 201 Created when the love is recorded;
    /// 400 Bad Request for invalid requests;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 409 Conflict if the product is already loved by the user;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPost("add-love-for-product/{productId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddLoveForProduct(string productId)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/add-love-for-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new AddLoveCommand(id, productId));

        return result.ToActionResult();
    }

    /// <summary>
    /// Removes a 'love' (like) from a product for the authenticated user.
    /// </summary>
    /// <param name="productId">Product identifier to remove love from.</param>
    /// <returns>
    /// 204 No Content when removal succeeds;
    /// 400 Bad Request for invalid requests;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 404 Not Found when the love record does not exist;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpDelete("remove-love-from-product/{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveLoveFromProduct(string productId)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/remove-love-from-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new RemoveLoveCommand(id, productId));

        return result.ToActionResult();
    }

    /// <summary>
    /// Adds a review for a product on behalf of the authenticated user.
    /// </summary>
    /// <param name="model">Review model containing product id, rating and review text.</param>
    /// <returns>
    /// 201 Created when the review is successfully created;
    /// 400 Bad Request for validation errors;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 409 Conflict if the user already reviewed the product or other uniqueness violation;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPost("add-review-for-product")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddReviewForProduct([FromBody] AddProductReviewModel model)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/add-review-for-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new AddProductReviewCommand(id, model.productId, model.Rating, model.Review));

        return result.ToActionResult();
    }

    /// <summary>
    /// Updates an existing product review.
    /// </summary>
    /// <param name="model">Model containing review id, new rating and review text.</param>
    /// <returns>
    /// 200 OK when the update succeeds;
    /// 400 Bad Request for validation errors;
    /// 404 Not Found when the review is not found;
    /// 409 Conflict when update causes a conflict;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpPut("update-review-for-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditReviewForProduct([FromBody] UpdateProductReviewModel model)
    {
        Result result = await _sender.Send(new UpdateProductReviewCommand(model.productReviewId, model.Rating, model.Review));

        return result.ToActionResult();
    }

    /// <summary>
    /// Removes a product review.
    /// </summary>
    /// <param name="Id">Identifier of the review to remove.</param>
    /// <returns>
    /// 204 No Content when deletion succeeds;
    /// 400 Bad Request for invalid requests;
    /// 404 Not Found when the review does not exist;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpDelete("remove-review-from-product/{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveReviewFromProduct(string Id)
    {
        Result result = await _sender.Send(new RemoveProductReviewCommand(Id));

        return result.ToActionResult();
    }
    
    /// <summary>
    /// Gets paged orders for the authenticated user.
    /// </summary>
    /// <param name="PageNumber">Page number (1-based).</param>
    /// <param name="PageSize">Page size (max per page).</param>
    /// <returns>
    /// 200 OK with paged order results when found;
    /// 204 No Content when there are no orders (search/filter semantics);
    /// 400 Bad Request for invalid paging parameters;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 404 Not Found when the user does not exist;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpGet("get-orders-by-user/{PageNumber},{PageSize}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType<PagedResult<GetOrderDetailsQueryResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrdersByUser(int PageNumber, byte PageSize)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/user/get-orders-by-user", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result<PagedResult<GetOrderDetailsQueryResponse>> result = await _sender.Send(new GetOrdersForUserQuery(id, PageNumber, PageSize));

        return result.ToActionResult();
    }

    /// <summary>
    /// Gets the authenticated user's wishlist with paging.
    /// </summary>
    /// <param name="PageNumber">Page number (1-based).</param>
    /// <param name="PageSize">Page size (max per page).</param>
    /// <returns>
    /// 200 OK with paged wishlist when items exist;
    /// 204 No Content when wishlist is empty;
    /// 400 Bad Request for invalid paging parameters;
    /// 401 Unauthorized when authentication token is missing or invalid;
    /// 404 Not Found when the user does not exist;
    /// 500 Internal Server Error for unexpected failures.
    /// </returns>
    [HttpGet("get-my-wishlist/{PageNumber},{PageSize}")]
    [ProducesResponseType<GetUserWishListQueryResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status204NoContent)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyWishlist(int PageNumber, byte PageSize)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/usre/get-my-wishlist", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result<PagedResult<GetUserWishListQueryResponse>> result = await _sender.Send(new GetUserWishListQuery(id, PageNumber, PageSize));

        return result.ToActionResult();
    }

  /*  [HttpGet("get-my-reviews{PageNumber},{PageSize}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyReviews()
    {
        throw new NotImplementedException();
    }*/

}
