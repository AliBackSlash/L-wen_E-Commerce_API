namespace Löwen.Presentation.API.Controllers.v1.EmailsController
{
    /// <summary>
    /// Handles email-related operations such as sending confirmation tokens.
    /// Targets API version 1.0 and exposes endpoints under "api/Email".
    /// </summary>
    /// <remarks>
    /// Uses an <see cref="ISender"/> instance (injected via the primary constructor) to dispatch commands/queries.
    /// </remarks>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Email")]
    public class EmailController(ISender _sender) : ControllerBase
    {
        /// <summary>
        /// Sends an email confirmation token to the specified email address.
        /// </summary>
        /// <param name="email">The recipient email address where the confirmation token will be sent.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the outcome.
        /// Possible responses:
        ///  - 201 Created: Token successfully created and queued for sending (returns <see cref="Result"/>).
        ///  - 400 Bad Request: Validation or input errors (returns <see cref="IEnumerable{Error}"/>).
        ///  - 409 Conflict: Duplicate or conflicting resource detected when creating the token (returns <see cref="IEnumerable{Error}"/>).
        ///  - 500 Internal Server Error: Unexpected failure (returns <see cref="IEnumerable{Error}"/>).
        /// </returns>
        [HttpPost("send-confirmation-email-token")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync(string email)
        {
            Result result = await _sender.Send(new EmailConfirmationTokenCommand(email));

            return result.ToActionResult();
        }

    }
}
