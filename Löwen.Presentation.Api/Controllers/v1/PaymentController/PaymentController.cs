using Löwen.Application.Features.PaymentFeature.AddPayment;
using Löwen.Application.Features.PaymentFeature.UpdatePaymentStatus;
using Löwen.Domain.Entities;
using Löwen.Presentation.Api.Controllers.v1.PaymentController.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.PaymentController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Payment")]
    public class PaymentController(ISender sender) : ControllerBase
    {
        [HttpPost("add-payment")]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPayment([FromBody] AddPaymentModel model)
        {
            Result result = await sender.Send(new AddPaymentCommand(model.OrderId, model.Amount,
                                                model.PaymentMethod, model.TransactionId, model.Status));
            return result.ToActionResult();
        }

        [HttpPut("update-payment-status/{Id},{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePaymentStatus(string Id, PaymentStatus status)
        {
            Result result = await sender.Send(new UpdatePaymentStatusCommand(Id,status));

            return result.ToActionResult();
        }

    }
}
