namespace Löwen.Presentation.API.Controllers.v1.AuthController
{
    /// <summary>
    /// Authentication endpoints for registering, logging in, confirming email and resetting password.
    /// All endpoints are anonymous and forward commands/queries to the application layer via <see cref="ISender"/>.
    /// </summary>
    [Route("api/Auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(ISender _sender ) : ControllerBase
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
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel request)
        {
            Result<LoginCommandResponse> result = await _sender.Send(new LoginCommand(request.UserNameOrEmail,request.Password));
 
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
        /// <param name="request">Payload containing email and the new Password.</param>
        /// <returns>
        /// 200 OK when password reset succeeds.
        /// 404 Not Found with a collection of <see cref="Error"/> if the user/email is not found.
        /// 409 Conflict with a collection of <see cref="Error"/> if there is a conflict preventing the update.
        /// 400 Bad Request with a collection of <see cref="Error"/> for validation errors.
        /// 500 Internal Server Error with a collection of <see cref="Error"/> for unexpected failures.
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
    }
}
