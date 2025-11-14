using Microsoft.AspNetCore.RateLimiting;

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
    [Authorize(Roles = "User,Admin")]

    public class EmailController(ISender _sender) : ControllerBase
    {
        /// <summary>
        /// Generates and queues an email confirmation token for the specified address.
        /// </summary>
        /// <remarks>
        /// Use this endpoint to start the email verification process. Common scenarios include:
        /// - After account creation to confirm a user's email.
        /// - When a user changes their email address and needs verification.
        /// - Resending a token if the initial confirmation email was not received.
        /// </remarks>
        /// <param name="email">The recipient email address. This value is provided via the query string and must be a valid email.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result:
        /// - 201 Created: Token generated and queued for sending.
        /// - 400 Bad Request: Validation or input errors (e.g., invalid or missing email).
        /// - 409 Conflict: Duplicate or pending token or already-confirmed email.
        /// - 500 Internal Server Error: Unexpected server error.
        /// </returns>
        [EnableRateLimiting("VerifyEmailPolicy")]

        [HttpPost("send-confirmation-email-token")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status429TooManyRequests)]

        public async Task<IActionResult> ConfirmEmailAsync(string email)
        {
            Result result = await _sender.Send(new EmailConfirmationTokenCommand(email));

            return result.ToActionResult();
        }

    }
}
