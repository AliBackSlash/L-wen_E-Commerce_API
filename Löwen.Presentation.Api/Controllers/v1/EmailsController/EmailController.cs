using Löwen.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;
using Löwen.Application.Features.SendEmailFeature.EmailConfirmationTokenCommand;
using Löwen.Application.Features.SendEmailFeature.SendResetTokenCommand;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Presentation.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.API.Controllers.v1.EmailsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController(ISender _sender) : ControllerBase
    {
        [HttpPost("send-confirmation-email-token")]
        [ProducesResponseType<ConfirmEmailResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync(string email)
        {
            Result result = await _sender.Send(new EmailConfirmationTokenCommand(email));

            return result.ToActionResult();
        }

        [HttpPost("send-Rest-Password-email-token")]
        [ProducesResponseType<ConfirmEmailResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RestPasswordemailAsync(string email)
        {
            Result result = await _sender.Send(new SendResetTokenCommand(email));

            return result.ToActionResult();
        }
    }
}
