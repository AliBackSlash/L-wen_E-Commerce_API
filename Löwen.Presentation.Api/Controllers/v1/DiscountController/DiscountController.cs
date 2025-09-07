using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.DiscountController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        [HttpPost("add-discount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDiscount()
        {
            throw new NotImplementedException();
        }

        [HttpPut("update-discount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDiscount()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("remove-discount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveDiscount()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-discount-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDiscountById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-all-discounts-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDiscountsPaged()
        {
            throw new NotImplementedException();
        }

        [HttpPost("assign-discount-to-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignDiscountToProduct()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("remove-discount-from-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveDiscountFromProduct()
        {
            throw new NotImplementedException();
        }

    }
}
