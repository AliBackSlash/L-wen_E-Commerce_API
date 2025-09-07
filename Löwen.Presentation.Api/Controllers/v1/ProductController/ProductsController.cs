using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.ProductController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("get-products-by-name-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByNamePaged()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-products-by-type-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByTypePaged()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-products-by-age-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByAgePaged()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-products-by-kind-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByKindPaged()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-all-products-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsPaged()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-product-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-product-by-id-with-reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductByIdWithReviews()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-products-by-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByCategory()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-products-by-tag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByTag()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-top-rated-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopRatedProducts()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-most-loved-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostLovedProducts()
        {
            throw new NotImplementedException();
        }

    }
}
