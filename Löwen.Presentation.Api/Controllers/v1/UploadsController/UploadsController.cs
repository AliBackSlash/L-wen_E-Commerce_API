using Löwen.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;
using Löwen.Application.Features.UploadFeature.UpdateProfileImageCommand;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Presentation.Api.Controllers.v1.UploadsController.Models;
using Löwen.Presentation.API.Extensions;
using Löwen.Presentation.API.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.UploadsController
{
    [Route("api/v1/Uploads")]
    [ApiController]
    public class UploadsController(ISender _sender, IFileService fileService) : ControllerBase
    {
 
        [HttpPut("Update-Profile-Image")]
        [ProducesResponseType<ConfirmEmailResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProfileImage([FromForm] UpdateProfileImageModel request)
        {

            var UploadResult = await fileService.UploadProfileImageAsync(request.Image);

            if (UploadResult is null)
                return Result.Failure(Error.BadRequest("Image upload error", "Profile image not uploaded")).ToActionResult();

            if (UploadResult.IsFailure)
                return UploadResult.ToActionResult();

            Result result = await _sender.Send(new UpdateProfileImageCommand(request.userId, UploadResult.Value.ImagePathWithoutRootPath, UploadResult.Value.CurrentRootPath));

            return result.ToActionResult();

        }
    }
}
