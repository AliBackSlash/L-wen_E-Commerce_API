using Löwen.Application.Features.RootAdminFeatures.Commands.AddAdmin;
using Löwen.Presentation.Api.Controllers.v1.RootAdminController.Models;
using Löwen.Presentation.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("assign-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRole()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("remove-admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAdmin()
        {
            throw new NotImplementedException();
        }

        [HttpGet("admin-by-id-or-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdminByIdOrEmail()
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
