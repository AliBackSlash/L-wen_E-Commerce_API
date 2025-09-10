using Löwen.Application.Features.RootAdminFeatures.Commands.ActivateMarkedAsDeleted;
using Löwen.Application.Features.RootAdminFeatures.Commands.AddAdmin;
using Löwen.Application.Features.RootAdminFeatures.Commands.AssignRole;
using Löwen.Application.Features.RootAdminFeatures.Commands.MarkAsDeleted;
using Löwen.Application.Features.RootAdminFeatures.Commands.RemoveAdminCommand;
using Löwen.Application.Features.RootAdminFeatures.Commands.RemoveRoleFromUser;
using Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;
using Löwen.Application.Features.UserFeature.Queries;
using Löwen.Domain.Enums;
using Löwen.Presentation.Api.Controllers.v1.RootAdminController.Models;
using Löwen.Presentation.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;
using System.Xml.Linq;

namespace Löwen.Presentation.Api.Controllers.v1.RootAdminController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> RemoveAdmin(Guid Id)
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
            Result<GetAdminByIdQueryResponse> result = await sender.Send(new GetAdminByIdQuery(Id, role));

            return result.ToActionResult();
        }

        [HttpGet("admin-by-email/{Email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdminByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        [HttpGet("admins")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdmins()
        {
            throw new NotImplementedException();
        }

    }
}
