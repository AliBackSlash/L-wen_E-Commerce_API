using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.NotificationController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //not impelement for v1
    public class NotificationController : ControllerBase
    {
        [HttpPost("create-notification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNotification()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-notification-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotificationById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-notifications-by-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotificationsByUser()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("delete-notification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNotification()
        {
            throw new NotImplementedException();
        }

    }
}
