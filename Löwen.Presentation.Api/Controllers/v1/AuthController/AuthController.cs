using Löwen.Application.Features.AuthFeature.Commands.RefreshToken;
using MediatR;

namespace Löwen.Presentation.API.Controllers.v1.AuthController
{
    /// <summary>
    /// Authentication endpoints for registering, logging in, confirming email and resetting password.
    /// All endpoints are anonymous and forward commands/queries to the application layer via <see cref="ISender"/>.
    /// </summary>
    [Route("api/Auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(ISender _sender) : ControllerBase
    {

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">Registration payload containing Email, UserName and Password.</param>
        /// <returns>
        /// 201 Created when registration succeeds.
        /// 409 Conflict with a collection of <see cref="Error"/> if a duplicate user/resource exists.
        /// 400 Bad Request with a collection of <see cref="Error"/> for validation errors.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel request)
        {
            Result result = await _sender.Send(new RegisterCommand(request.Email, request.UserName, request.Password));

            return result.ToActionResult();
        }

        /// <summary>
        /// Authenticates a user and returns authentication data (e.g. tokens) on success.
        /// </summary>
        /// <param name="request">Login payload containing UserNameOrEmail and Password.</param>
        /// <returns>
        /// 200 OK with <see cref="LoginCommandResponse"/> when authentication succeeds.
        /// 400 Bad Request with a collection of <see cref="Error"/> for validation errors.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel request)
        {
            Result<LoginCommandResponse> result = await _sender.Send(new LoginCommand(request.UserNameOrEmail, request.Password));

            return result.ToActionResult();
        }

        /// <summary>
        /// Confirms a user's email using userId and token supplied as query parameters.
        /// </summary>
        /// <param name="request">Query payload containing userId and confirmEmailToken.</param>
        /// <returns>
        /// 200 OK with <see cref="ConfirmEmailResponse"/> when confirmation succeeds.
        /// 404 Not Found with a collection of <see cref="Error"/> if the user or token was not found/invalid.
        /// 400 Bad Request with a collection of <see cref="Error"/> for validation errors.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpGet("confirm-email")]
        [ProducesResponseType(typeof(ConfirmEmailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailModel request)
        {
            Result<ConfirmEmailResponse> result = await _sender.Send(new ConfirmEmailCommand(request.userId, request.confirmEmailToken));

            return result.ToActionResult();
        }

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <remarks>
        /// This endpoint allows a user to reset their password using a valid email and new password payload.
        /// The server will validate the provided email, ensure it corresponds to an existing user account, 
        /// and enforce password complexity policies before updating the user's password.
        ///
        /// Security considerations:
        ///  - The new password must comply with the system's password policy (minimum length, complexity).
        ///  - The endpoint should be protected against brute-force and enumeration attacks.
        ///  - Passwords must be transmitted securely (HTTPS) and never logged in plaintext.
        ///
        /// Typical client flow:
        ///  1. User requests a password reset (e.g., via "Forgot Password" form).
        ///  2. Client calls this endpoint with the email and new password.
        ///  3. Server validates the request and updates the password.
        ///  4. Optionally, the server may notify the user via email about the password change.
        /// </remarks>
        /// <param name="request">
        /// Payload containing:
        ///  - Email: The user's registered email address. Required.
        ///  - Password: The new password to set. Must comply with password policies.
        /// </param>
        /// <returns>
        /// 200 OK when the password is successfully reset.
        /// 400 Bad Request if the payload is malformed or password does not meet policy requirements.
        /// 404 Not Found if the email does not correspond to any user.
        /// 500 Internal Server Error for unexpected server-side failures.
        /// </returns>

        [HttpPut("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RestPasswordAsync([FromBody] ResetPasswordModel request)
        {
            Result result = await _sender.Send(new ResetPasswordCommand(request.email,
                request.Password));

            return result.ToActionResult();
        }

        /// <summary>
        /// Exchanges a valid refresh token for a new access token.
        /// </summary>
        /// <remarks>
        /// This endpoint accepts a refresh token (provided as a path parameter) previously issued by the authentication service.
        /// The server will validate the token, ensure it has not been revoked or expired, and — when valid — issue a new short-lived access token
        /// in the response body. Refresh tokens are sensitive credentials and must not be leaked (do not log them).
        ///
        /// Typical client flow:
        ///  - Client detects an expired/invalid access token.
        ///  - Client calls this endpoint with the stored refresh token.
        ///  - Server validates the refresh token, checks revocation/rotation policies, and returns a new access token (and optionally a rotated refresh token).
        /// </remarks>
        /// <param name="refreshToken">
        /// The refresh token string to exchange for a new access token. Required; must be a non-empty token issued by the authentication system.
        /// The token format (opaque or JWT) depends on the server implementation.
        /// </param>
        /// <returns>
        /// 200 OK with <see cref="RefreshTokenCommandResponse"/> containing a new access token when the refresh token is valid.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request is malformed (for example, missing or empty token).
        /// 401 Unauthorized with a collection of <see cref="Error"/> when the refresh token is invalid, expired, revoked, or authentication fails.
        /// 404 Not Found with a collection of <see cref="Error"/> when the user or session associated with the token cannot be found.
        /// 409 Conflict with a collection of <see cref="Error"/> when token rotation or concurrency conflicts prevent issuing a token.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected server-side failures.
        /// </returns>
        [HttpPut("refresh-token/{refreshToken}")]
        [ProducesResponseType(typeof(RefreshTokenCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            Result<RefreshTokenCommandResponse> result = await _sender.Send(new RefreshTokenCommand(refreshToken));

            return result.ToActionResult();
        }
    }
}
