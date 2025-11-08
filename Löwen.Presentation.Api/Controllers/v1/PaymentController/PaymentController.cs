namespace Löwen.Presentation.Api.Controllers.v1.PaymentController
{
    /// <summary>
    /// Controller that exposes payment-related endpoints (v1).
    /// </summary>
    /// <remarks>
    /// Route: api/Payment
    /// This controller delegates work to an <see cref="ISender"/> to execute application commands/queries.
    /// </remarks>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Payment")]
    public class PaymentController(ISender sender) : ControllerBase
    {
        
        /// <summary>
        /// Retrieves a single payment by its identifier.
        /// </summary>
        /// <returns>
        /// 200 OK with a <see cref="Result"/> containing the payment when found.
        /// 400 Bad Request with an enumerable of <see cref="Error"/> when the request is invalid.
        /// 404 Not Found with an enumerable of <see cref="Error"/> when the payment does not exist.
        /// 500 Internal Server Error with an enumerable of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpGet("get-payment-by-id")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaymentById()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves payments for a specific order.
        /// </summary>
        /// <remarks>
        /// This endpoint is intended to return all payments associated with an order (search/filter style).
        /// </remarks>
        /// <returns>
        /// 200 OK with a <see cref="Result"/> when one or more payments are found.
        /// 204 No Content when there are no matching payments for the specified order.
        /// 400 Bad Request with an enumerable of <see cref="Error"/> when the request is invalid.
        /// 500 Internal Server Error with an enumerable of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpGet("get-payments-by-order")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaymentsByOrder()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creates a new payment for an order.
        /// </summary>
        /// <param name="model">The payment data to create.</param>
        /// <returns>
        /// 201 Created with a <see cref="Result"/> when the payment is successfully created.
        /// 400 Bad Request with an enumerable of <see cref="Error"/> when the request is invalid.
        /// 409 Conflict with an enumerable of <see cref="Error"/> when a duplicate or conflicting resource exists.
        /// 500 Internal Server Error with an enumerable of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPost("add-payment")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPayment([FromBody] AddPaymentModel model)
        {
            Result result = await sender.Send(new AddPaymentCommand(model.OrderId, model.Amount,
                                                model.PaymentMethod, model.TransactionId, model.Status));
            return result.ToActionResult();
        }

        /// <summary>
        /// Updates the status of an existing payment.
        /// </summary>
        /// <param name="Id">The identifier of the payment to update.</param>
        /// <param name="status">The new <see cref="PaymentStatus"/> to apply to the payment.</param>
        /// <returns>
        /// 200 OK with a <see cref="Result"/> when the update succeeds.
        /// 400 Bad Request with an enumerable of <see cref="Error"/> when the request is invalid.
        /// 404 Not Found with an enumerable of <see cref="Error"/> when the payment does not exist.
        /// 409 Conflict with an enumerable of <see cref="Error"/> when the update conflicts with current state.
        /// 500 Internal Server Error with an enumerable of <see cref="Error"/> for unexpected failures.
        /// </returns>
        [HttpPut("update-payment-status/{Id},{status}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePaymentStatus(string Id, PaymentStatus status)
        {
            Result result = await sender.Send(new UpdatePaymentStatusCommand(Id,status));

            return result.ToActionResult();
        }

    }
}
