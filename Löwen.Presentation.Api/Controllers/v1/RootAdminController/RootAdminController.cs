namespace Löwen.Presentation.Api.Controllers.v1.RootAdminController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/RootAdmin")]
    public class RootAdminController(ISender sender) : ControllerBase
    {
        [HttpPost("add-admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAdmin([FromBody] AddAdminModel request)
        {
            Result result = await sender.Send(new AddAdminCommand(request.Email, request.UserName, request.Password,
                    request.FName, request.MName, request.LName, request.DateOfBirth, request.PhoneNumber, request.Gender));

            return result.ToActionResult();
        }

        [HttpPost("assign-role/{Id:guid},{role:max(4)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRole(Guid Id,UserRole role)
        {
            Result result = await sender.Send(new AssignRoleCommand(Id, role));

            return result.ToActionResult(); 
        }

        [HttpDelete("Remove-Role-From-User-admin/{Id:guid},{role:max(4)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveRoleFromUser(Guid Id,UserRole role)
        {
            Result result = await sender.Send(new RemoveRoleFromUserCommand(Id, role));

            return result.ToActionResult(); 
        }

        [HttpPut("mark-admin-as-deleted/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkAdminAsDeleted(Guid Id)
        {
            Result result = await sender.Send(new MarkAdminAsDeletedCommand(Id));

            return result.ToActionResult();
        }

        [HttpPut("activate-marked-as-deleted/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateMarkedAsDeleted(Guid Id)
        {
            Result result = await sender.Send(new ActivateMarkedAsDeletedCommand(Id));

            return result.ToActionResult();
        }

        [HttpDelete("remove-admin/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAdmin(string Id)
        {
            Result result = await sender.Send(new RemoveAdminCommand(Id));

            return result.ToActionResult();
        }


        [HttpGet("admin-by-id/{Id:guid},{role:max(4)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdminById(Guid Id,UserRole role)
        {
            Result<GetUserQueryResponse> result = await sender.Send(new GetAdminByIdQuery(Id, role));

            return result.ToActionResult();
        }

        [HttpGet("admin-by-email/{Email},{role:max(4)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdminByEmail(string Email, UserRole role)
        {
            Result<GetUserQueryResponse> result = await sender.Send(new GetAdminByEmailQuery(Email, role));

            return result.ToActionResult();
        }

        [HttpGet("admins/{role:max(4)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdmins(UserRole role)
        {
            Result<List<GetdminsQueryResponse>> result = await sender.Send(new GetAdminsQuery(role));

            return result.ToActionResult();
        }

    }
}
