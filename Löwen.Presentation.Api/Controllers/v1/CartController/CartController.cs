using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.CartController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        [HttpPost("add-to-cart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToCart()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("remove-from-cart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFromCart()
        {
            throw new NotImplementedException();
        }

        [HttpPut("update-cart-item-quantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCartItemQuantity()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-cart-by-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartByUser()
        {
            throw new NotImplementedException();
        }

    }
}
