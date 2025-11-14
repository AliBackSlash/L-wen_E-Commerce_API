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
        /// <remarks>
        /// This endpoint creates a new user account in the system. It is used during the user registration/sign-up flow.
        /// The server validates all input fields for correctness and uniqueness, ensuring no duplicate users exist with the same email or username.
        ///
        /// Action usage:
        ///  - Called by clients during the sign-up process.
        ///  - Typically followed by an email confirmation step (see <see cref="ConfirmEmailAsync"/>).
        ///  - Should enforce strong password policies and rate limiting to prevent abuse.
        ///
        /// Parameter binding:
        ///  - The request parameter is bound from the HTTP request body (JSON) using <c>[FromBody]</c> attribute.
        ///  - The entire registration model is deserialized from the request body.
        /// </remarks>
        /// <param name="request">
        /// Registration payload (body parameter) containing:
        ///  - Email: The user's email address. Must be unique and valid format.
        ///  - UserName: The user's desired username. Must be unique and meet naming requirements.
        ///  - Password: The user's password. Must comply with password complexity policies.
        /// Binding: <c>[FromBody]</c> — passed as JSON in the HTTP request body.
        /// </param>
        /// <returns>
        /// 201 Created when registration succeeds; returns the created user resource.
        /// 409 Conflict with a collection of <see cref="Error"/> if a duplicate user/resource exists (email or username already taken).
        /// 400 Bad Request with a collection of <see cref="Error"/> for validation errors (invalid format, missing fields, policy violations).
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
        /// <remarks>
        /// This endpoint handles user login and authentication. It validates the provided credentials against the user database
        /// and, upon successful authentication, returns authentication tokens (access token, refresh token, etc.).
        ///
        /// Action usage:
        ///  - Called by clients during the login/sign-in flow.
        ///  - Marks the start of an authenticated session for the user.
        ///  - The returned tokens are used for subsequent authenticated requests to the API.
        ///  - Should be rate-limited to prevent brute-force attacks.
        ///
        /// Parameter binding:
        ///  - The request parameter is bound from the HTTP request body (JSON) using <c>[FromBody]</c> attribute.
        ///  - Clients send login credentials as JSON in the request body.
        /// </remarks>
        /// <param name="request">
        /// Login payload (body parameter) containing:
        ///  - UserNameOrEmail: The user's username or registered email address.
        ///  - Password: The user's password (plain text, must be transmitted over HTTPS).
        /// Binding: <c>[FromBody]</c> — passed as JSON in the HTTP request body.
        /// </param>
        /// <returns>
        /// 200 OK with <see cref="LoginCommandResponse"/> containing authentication tokens (access token, refresh token, user info, etc.) when authentication succeeds.
        /// 400 Bad Request with a collection of <see cref="Error"/> for validation errors (missing fields, invalid format).
        /// 401 Unauthorized with a collection of <see cref="Error"/> when credentials are invalid or user not found.
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
        /// <remarks>
        /// This endpoint verifies and confirms a user's email address using an email confirmation token.
        /// Typically, a confirmation token is generated and sent to the user's email during registration.
        /// The user clicks a link containing this endpoint with the token and userId, which confirms ownership of the email.
        ///
        /// Action usage:
        ///  - Called by users after registration to verify their email address (typically via email link).
        ///  - Required step in the user registration flow to activate the account.
        ///  - Once confirmed, the user can fully access the system.
        ///  - Tokens are typically short-lived (e.g., 24 hours) to ensure timely confirmation.
        ///
        /// Parameter binding:
        ///  - Both parameters are bound from the HTTP query string using <c>[FromQuery]</c> attribute.
        ///  - Parameters appear in the URL as <c>?userId=...&amp;confirmEmailToken=...</c>.
        /// </remarks>
        /// <param name="request">
        /// Query payload (URL query parameters) containing:
        ///  - userId: The unique identifier of the user whose email is being confirmed.
        ///  - confirmEmailToken: The email confirmation token (typically a long alphanumeric string) sent to the user's email address.
        /// Binding: <c>[FromQuery]</c> — passed as URL query string parameters (e.g., <c>/confirm-email?userId=123&amp;confirmEmailToken=abc...</c>).
        /// </param>
        /// <returns>
        /// 200 OK with <see cref="ConfirmEmailResponse"/> containing user information or confirmation status when email confirmation succeeds.
        /// 404 Not Found with a collection of <see cref="Error"/> if the user or token was not found/invalid (token expired or userId does not exist).
        /// 400 Bad Request with a collection of <see cref="Error"/> for validation errors (malformed userId, missing/empty token).
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPost("confirm-email")]
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
        /// This endpoint allows a user to reset their password. It is typically used in a "Forgot Password" flow
        /// where the user requests a password reset without providing the old password.
        /// The server will validate the provided email, ensure it corresponds to an existing user account, 
        /// and enforce password complexity policies before updating the user's password.
        ///
        /// Action usage:
        ///  - Called during "Forgot Password" or "Password Reset" flows.
        ///  - Allows users to regain access to their accounts without the old password.
        ///  - Should be protected against brute-force and enumeration attacks (e.g., rate limiting, consistent response times).
        ///  - Optionally sends a confirmation email to the user notifying them of the password change.
        ///
        /// Security considerations:
        ///  - The new password must comply with the system's password policy (minimum length, complexity, character diversity).
        ///  - The endpoint should be rate-limited to prevent brute-force attempts and abuse.
        ///  - Consider requiring additional verification steps (e.g., OTP, email confirmation token) before allowing password reset.
        ///  - Passwords must be transmitted securely (HTTPS only) and never logged in plaintext.
        ///  - Avoid enumerating user accounts by using consistent response messages.
        ///
        /// Parameter binding:
        ///  - The request parameter is bound from the HTTP request body (JSON) using <c>[FromBody]</c> attribute.
        /// </remarks>
        /// <param name="request">
        /// Payload (body parameter) containing:
        ///  - Email: The user's registered email address. Required; used to identify the user account.
        ///  - Password: The new password to set. Must comply with password policies (minimum length, complexity).
        /// Binding: <c>[FromBody]</c> — passed as JSON in the HTTP request body.
        /// </param>
        /// <returns>
        /// 200 OK when the password is successfully reset; may optionally return confirmation details.
        /// 400 Bad Request if the payload is malformed or the new password does not meet policy requirements.
        /// 404 Not Found if the email does not correspond to any user in the system.
        /// 409 Conflict if there are policy conflicts or concurrent modification issues.
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
        /// This endpoint is part of the token refresh mechanism. When a client's access token expires,
        /// it can use a previously issued refresh token to obtain a new access token without requiring
        /// the user to log in again. The server validates the refresh token, checks revocation/rotation policies,
        /// and — when valid — issues a new short-lived access token.
        ///
        /// Action usage:
        ///  - Called by clients when their access token has expired or is about to expire.
        ///  - Allows users to maintain session continuity without re-authentication.
        ///  - Part of the OAuth 2.0 token refresh flow.
        ///  - Can include token rotation logic to invalidate the old refresh token and issue a new one.
        ///
        /// Security considerations:
        ///  - Refresh tokens are sensitive credentials and must not be logged or exposed in debug output.
        ///  - The server should enforce token rotation: invalidate old refresh tokens after issuing new ones.
        ///  - Implement revocation checks to prevent use of compromised or revoked tokens.
        ///  - Consider short expiration windows for refresh tokens (e.g., days to weeks).
        ///  - Rate limit this endpoint to detect token brute-force attacks.
        ///  - Always use HTTPS to protect token transmission.
        ///
        /// Parameter binding:
        ///  - The refreshToken parameter is bound from the URL path using <c>[FromRoute]</c> (implicit via route definition).
        ///  - The token is extracted from the route segment: <c>/refresh-token/{refreshToken}</c>.
        /// </remarks>
        /// <param name="refreshToken">
        /// The refresh token string to exchange for a new access token (URL path parameter). 
        /// Required; must be a non-empty, valid token previously issued by the authentication system.
        /// The token format (opaque or JWT) depends on the server implementation.
        /// Binding: <c>[FromRoute]</c> (implicit) — extracted from the URL path segment <c>{refreshToken}</c>.
        /// </param>
        /// <returns>
        /// 200 OK with <see cref="RefreshTokenCommandResponse"/> containing a new access token (and optionally a rotated refresh token) when the refresh token is valid.
        /// 400 Bad Request with a collection of <see cref="Error"/> when the request is malformed (missing or empty token).
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
