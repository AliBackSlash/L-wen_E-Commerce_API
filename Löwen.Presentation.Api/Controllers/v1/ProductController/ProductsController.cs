using Löwen.Application.Features.ProductFeature.Queries;
using Löwen.Application.Features.ProductFeature.Queries.GetAllProductPaged;
using Löwen.Application.Features.ProductFeature.Queries.GetProductById;

namespace Löwen.Presentation.Api.Controllers.v1.ProductController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Product")]
    public class ProductController(ISender sender) : ControllerBase
    {
        [HttpGet("get-all-products-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType<PagedResult<GetProductQueryResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsPaged(int PageNumber,byte PageSize)
        {
            Result<PagedResult<GetProductQueryResponse>> result = await sender.Send(new GetAllProductPagedQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }


        [HttpGet("get-products-by-name-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByNamePaged()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-products-by-gender-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByGenderPaged()
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


        [HttpGet("get-product-by-id/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById(string productId)
        {
            Result<GetProductByIdQueryResponse> result = await sender.Send(new GetProductByIdQuery(productId));

            return result.ToActionResult();
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

        [HttpGet("get-products-with-all-filters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsWithAllFiltersPaged()
        {
            throw new NotImplementedException();
        }
    }
}
