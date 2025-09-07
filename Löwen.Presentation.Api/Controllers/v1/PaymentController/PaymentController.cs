using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.PaymentController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost("add-payment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPayment()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-payment-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaymentById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-payments-by-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaymentsByOrder()
        {
            throw new NotImplementedException();
        }

        [HttpPut("update-payment-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePaymentStatus()
        {
            throw new NotImplementedException();
        }

    }
}
