using Löwen.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;
using Löwen.Application.Features.AuthFeature.Commands.LoginCommand;
using Löwen.Application.Features.AuthFeature.Commands.RegisterCommand;
using Löwen.Application.Features.AuthFeature.Commands.ResetPasswordCommand;
using Löwen.Domain.Abstractions.IServices;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Infrastructure.Services.EmailServices;
using Löwen.Presentation.API.Controllers.v1.AuthController.Models;
using Löwen.Presentation.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Löwen.Presentation.API.Controllers.v1.AuthController
{
    [Route("api/Auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(ISender _sender ) : ControllerBase
    {

        [HttpPost("register")]
        [ProducesResponseType<RegisterCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel request)
        {
            Result<RegisterCommandResponse> result = await _sender.Send(new RegisterCommand(request.Email, request.UserName, request.Password));

            return result.ToActionResult();
        }

        [HttpPost("login")]
        [ProducesResponseType<LoginCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel request)
        {
            Result<LoginCommandResponse> result = await _sender.Send(new LoginCommand(request.UserNameOrEmail,request.Password));

            return result.ToActionResult();
        }

        [HttpPost("confirm-email")]
        [ProducesResponseType<ConfirmEmailResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailModel request)
        {
            Result<ConfirmEmailResponse> result = await _sender.Send(new ConfirmEmailCommand(request.userId, request.token));

            return result.ToActionResult();
        }

        [HttpPost("rest-password")]
        [ProducesResponseType<ConfirmEmailResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RestPasswordAsync([FromBody] ResetPasswordModel request)
        {
            Result<ResetPasswordCommandResponse> result = await _sender.Send(new ResetPasswordCommand(request.Email, request.token,
                request.Password,request.ConfermPassword));

            return result.ToActionResult();
        }
    }
}
