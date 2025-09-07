using Löwen.Application.Features.UserFeature.Commands.ChangePasswordCommand;
using Löwen.Application.Features.UserFeature.Commands.UpdateUserInfoCommand;
using Löwen.Application.Features.UserFeature.Queries;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Presentation.Api.Controllers.v1.UsersController.Models;
using Löwen.Presentation.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.UsersController;
[ApiController]
[ApiVersion("1.0")]
[Route("api/Users")]
public class UsersController(ISender _sender) : ControllerBase
{
    [HttpGet("/{token}")]
    [ProducesResponseType<GetUserByIdQueryResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserInfo(string token)
    {
        Result<GetUserByIdQueryResponse> result = await _sender.Send(new GetUserByIdQuery(token));

        return result.ToActionResult();
    }

    [HttpPut("Update")]
    [ProducesResponseType<UpdateUserInfoCommandResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserModel request)
    {
        Result<UpdateUserInfoCommandResponse> result = await _sender.Send(new UpdateUserInfoCommand
            (request.token, request.FName, request.MName, request.LName, request.phoneNumber));

        return result.ToActionResult();
    }



    [HttpPut("Change-Password")]
    [ProducesResponseType<ChangePasswordCommandResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordModel request)
    {
        Result<ChangePasswordCommandResponse> result = await _sender.Send(new ChangePasswordCommand(request.token, request.CurrentPassword,
            request.Password, request.ConfermPassword));

        return result.ToActionResult();
    }
}
