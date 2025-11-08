namespace Löwen.Presentation.Api.Controllers.v1.RootAdminController
{
    /// <summary>
    /// Controller for performing root-level administrative operations such as creating admins,
    /// assigning and removing roles, soft-deleting and restoring admins, and querying admin users.
    /// Returns domain Result objects and standardized error payloads where applicable.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/RootAdmin")]
    public class RootAdminController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Create a new admin user.
        /// </summary>
        /// <param name="request">Payload containing admin user information to create.</param>
        /// <returns>
        /// 201 Created with a Result on success.
        /// 400 Bad Request with validation errors when the request is invalid.
        /// 409 Conflict when a duplicate resource (for example email or username) already exists.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpPost("add-admin")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAdmin([FromBody] AddAdminModel request)
        {
            Result result = await sender.Send(new AddAdminCommand(request.Email, request.UserName, request.Password,
                    request.FName, request.MName, request.LName, request.DateOfBirth, request.PhoneNumber, request.Gender));

            return result.ToActionResult();
        }

        /// <summary>
        /// Assign a role to the specified user.
        /// </summary>
        /// <param name="Id">The user's identifier.</param>
        /// <param name="role">The role to assign to the user.</param>
        /// <returns>
        /// 200 OK with a Result on success.
        /// 400 Bad Request when the request is invalid.
        /// 404 Not Found when the user does not exist.
        /// 409 Conflict when assignment cannot be completed due to a conflicting state.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpPost("assign-role/{Id:guid},{role:max(4)}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRole(Guid Id,UserRole role)
        {
            Result result = await sender.Send(new AssignRoleCommand(Id, role));

            return result.ToActionResult(); 
        }

        /// <summary>
        /// Remove a role from the specified user.
        /// </summary>
        /// <param name="Id">The user's identifier.</param>
        /// <param name="role">The role to remove from the user.</param>
        /// <returns>
        /// 204 No Content when the role was successfully removed.
        /// 404 Not Found when the user or role assignment does not exist.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpDelete("Remove-Role-From-User-admin/{Id:guid},{role:max(4)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveRoleFromUser(Guid Id,UserRole role)
        {
            Result result = await sender.Send(new RemoveRoleFromUserCommand(Id, role));

            return result.ToActionResult(); 
        }

        /// <summary>
        /// Mark an admin user as deleted (soft-delete).
        /// </summary>
        /// <param name="Id">The identifier of the admin to mark as deleted.</param>
        /// <returns>
        /// 200 OK with a Result on success.
        /// 400 Bad Request when the request is invalid.
        /// 404 Not Found when the specified admin does not exist.
        /// 409 Conflict when the operation cannot be completed due to a conflicting state.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpPut("mark-admin-as-deleted/{Id:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkAdminAsDeleted(Guid Id)
        {
            Result result = await sender.Send(new MarkAdminAsDeletedCommand(Id));

            return result.ToActionResult();
        }

        /// <summary>
        /// Activate (restore) an admin previously marked as deleted.
        /// </summary>
        /// <param name="Id">The identifier of the admin to reactivate.</param>
        /// <returns>
        /// 200 OK with a Result on success.
        /// 400 Bad Request when the request is invalid.
        /// 404 Not Found when the specified admin does not exist.
        /// 409 Conflict when the operation conflicts with current state.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpPut("activate-marked-as-deleted/{Id:guid}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateMarkedAsDeleted(Guid Id)
        {
            Result result = await sender.Send(new ActivateMarkedAsDeletedCommand(Id));

            return result.ToActionResult();
        }

        /// <summary>
        /// Permanently remove an admin by id.
        /// </summary>
        /// <param name="Id">The identifier of the admin to remove.</param>
        /// <returns>
        /// 204 No Content when the admin was successfully removed.
        /// 404 Not Found when the admin does not exist.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpDelete("remove-admin/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAdmin(string Id)
        {
            Result result = await sender.Send(new RemoveAdminCommand(Id));

            return result.ToActionResult();
        }


        /// <summary>
        /// Retrieve an admin by identifier and role.
        /// </summary>
        /// <param name="Id">The user's identifier.</param>
        /// <param name="role">Role filter to ensure the returned user matches the expected admin role.</param>
        /// <returns>
        /// 200 OK with a Result containing the admin data when found.
        /// 400 Bad Request when the request is invalid.
        /// 404 Not Found when the admin is not found.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpGet("admin-by-id/{Id:guid},{role:max(4)}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdminById(Guid Id,UserRole role)
        {
            Result<GetUserQueryResponse> result = await sender.Send(new GetAdminByIdQuery(Id, role));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieve an admin by email and role.
        /// </summary>
        /// <param name="Email">The admin email address to look up.</param>
        /// <param name="role">Role filter to ensure the returned user matches the expected admin role.</param>
        /// <returns>
        /// 200 OK with a Result containing the admin data when found.
        /// 400 Bad Request when the request is invalid.
        /// 404 Not Found when the admin is not found.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpGet("admin-by-email/{Email},{role:max(4)}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdminByEmail(string Email, UserRole role)
        {
            Result<GetUserQueryResponse> result = await sender.Send(new GetAdminByEmailQuery(Email, role));

            return result.ToActionResult();
        }

        /// <summary>
        /// Get a list of admins filtered by role.
        /// </summary>
        /// <param name="role">The role to filter admins by.</param>
        /// <returns>
        /// 200 OK with a Result containing a list of admins when matches exist.
        /// 204 No Content when there are no matching admins.
        /// 400 Bad Request when the request is invalid.
        /// 500 Internal Server Error on unexpected failures.
        /// </returns>
        [HttpGet("admins/{role:max(4)}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdmins(UserRole role)
        {
            Result<List<GetdminsQueryResponse>> result = await sender.Send(new GetAdminsQuery(role));

            return result.ToActionResult();
        }

    }
}
