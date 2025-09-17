using Löwen.Application.Features.SendEmailFeature.EmailConfirmationTokenCommand;
using Löwen.Application.Features.UploadFeature.UpdateProfileImageCommand;
using Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;
using Löwen.Application.Features.UserFeature.Commands.UpdateUserInfoCommand;
using Löwen.Application.Features.UserFeature.Queries.GetUserById;
using Löwen.Presentation.Api.Controllers.v1.UsersController.Models;
using System.Security.Claims;
using System.IO;
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
            (id, request.FName, request.MName, request.LName,request.DateOfBirth, request.PhoneNumber,request.Gender));

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

        Result result = await _sender.Send(new UpdateProfileImageCommand(id, UploadResult.Value.ImagePathWithoutRootPath, UploadResult.Value.CurrentRootPath));

        if(result.IsFailure)
           System.IO.File.Delete(Path.Combine(UploadResult.Value.CurrentRootPath, UploadResult.Value.ImagePathWithoutRootPath));

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

    [HttpPost("add-product-to-wishlist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddProductToWishlist()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("remove-product-from-wishlist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveProductFromWishlist()
    {
        throw new NotImplementedException();
    }

    [HttpPost("add-love-for-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddLoveForProduct()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("remove-love-from-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveLoveFromProduct()
    {
        throw new NotImplementedException();
    }

    [HttpPost("add-review-for-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddReviewForProduct()
    {
        throw new NotImplementedException();
    }

    [HttpPut("update-review-for-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateReviewForProduct()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("remove-review-from-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveReviewFromProduct()
    {
        throw new NotImplementedException();
    }

    [HttpGet("get-my-orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyOrders()
    {
        throw new NotImplementedException();
    }

    [HttpGet("get-my-wishlist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyWishlist()
    {
        throw new NotImplementedException();
    }

    [HttpGet("get-my-reviews")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyReviews()
    {
        throw new NotImplementedException();
    }

}
