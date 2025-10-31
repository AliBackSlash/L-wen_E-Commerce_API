using Löwen.Application.Features.AdminFeature.Commands.Product.DeleteProductImages;
using Löwen.Application.Features.OrderFeature.Queries.OrderDetailsResponse;
using Löwen.Application.Features.UserFeature.Commands.DeleteUserImage;
using Löwen.Application.Features.UserFeature.Commands.ProductReviewOper.AddProductReview;
using Löwen.Application.Features.UserFeature.Commands.ProductReviewOper.UpdateProductReview;
using Löwen.Application.Features.UserFeature.Commands.UserInfoOper.ChangePassword;
using Löwen.Application.Features.UserFeature.Commands.UserInfoOper.UpdateUserInfo;
using Löwen.Application.Features.UserFeature.Commands.WishlistOper.RemoveProductReview;
using Löwen.Application.Features.UserFeature.Queries.GetOrdersForUser;
using Löwen.Application.Features.UserFeature.Queries.GetUserWishList;
using Löwen.Domain.Entities;

namespace Löwen.Presentation.Api.Controllers.v1.UsersController;
[ApiController]
[ApiVersion("1.0")]
[Route("api/users")]
//[Authorize(Roles = "User")]
public class UsersController(ISender _sender, IFileService fileService) : ControllerBase
{
    [HttpGet("get-user-info")]
    [ProducesResponseType<GetUserByIdQueryResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserInfo()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))      
            return Result.Failure(new Error("api/users/get-user-info", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();
        

        Result<GetUserByIdQueryResponse> result = await _sender.Send(new GetUserByIdQuery(id));

        return result.ToActionResult();
    }

    [HttpPut("update-user-info")]
    [ProducesResponseType<UpdateUserInfoCommandResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status401Unauthorized)]
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



    [HttpPut("change-password")]
    [ProducesResponseType<ChangePasswordCommandResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
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

    [HttpPut("update-profile-image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
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


    [HttpPut("remove-user-image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
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
    [HttpPost("verify-own-email/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
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

    [HttpPost("add-product-to-wishlist/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddProductToWishlist(string productId)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/add-product-to-wishlist", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new AddToWishlistCommand(id,productId));

        return result.ToActionResult();
    }

    [HttpDelete("remove-product-from-wishlist/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveProductFromWishlist(string productId)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/remove-product-from-wishlist", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new RemoveFromWishlistCommand(id, productId));

        return result.ToActionResult();
    }

    [HttpPost("add-love-for-product/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddLoveForProduct(string productId)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/add-love-for-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new AddLoveCommand(id, productId));

        return result.ToActionResult();
    }

    [HttpDelete("remove-love-from-product/{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveLoveFromProduct(string productId)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/remove-love-from-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new RemoveLoveCommand(id, productId));

        return result.ToActionResult();
    }

    [HttpPost("add-review-for-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddReviewForProduct([FromBody] AddProductReviewModel model)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/users/add-review-for-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result result = await _sender.Send(new AddProductReviewCommand(id, model.productId, model.Rating, model.Review));

        return result.ToActionResult();
    }

    [HttpPut("update-review-for-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditReviewForProduct([FromBody] UpdateProductReviewModel model)
    {
        Result result = await _sender.Send(new UpdateProductReviewCommand(model.productReviewId, model.Rating, model.Review));

        return result.ToActionResult();
    }

    [HttpDelete("remove-review-from-product/{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveReviewFromProduct(string Id)
    {
        Result result = await _sender.Send(new RemoveProductReviewCommand(Id));

        return result.ToActionResult();
    }
    
    [HttpGet("get-orders-by-user/{PageNumber},{PageSize}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrdersByUser(int PageNumber, byte PageSize)
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(id))
            return Result.Failure(new Error("api/user/get-orders-by-user", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

        Result<PagedResult<GetOrderDetailsQueryResponse>> result = await _sender.Send(new GetOrdersForUserQuery(id, PageNumber, PageSize));

        return result.ToActionResult();
    }

    [HttpGet("get-my-wishlist/{PageNumber},{PageSize}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
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
