namespace Löwen.Presentation.API.Controllers.v1.EmailsController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Email")]
    public class EmailController(ISender _sender) : ControllerBase
    {
        [HttpPost("send-confirmation-email-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync(string email)
        {
            Result result = await _sender.Send(new EmailConfirmationTokenCommand(email));

            return result.ToActionResult();
        }

        [HttpPost("send-verification-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendVerificationOrder()
        {
            throw new NotImplementedException();
        }

        [HttpPost("send-cancel-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendCancelOrder()
        {
            throw new NotImplementedException();
        }

    }
}
