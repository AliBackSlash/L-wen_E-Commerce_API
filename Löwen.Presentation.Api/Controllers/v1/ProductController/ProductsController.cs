namespace Löwen.Presentation.Api.Controllers.v1.ProductController
{
    /// <summary>
    /// API controller that exposes product-related read endpoints (listing, searching and retrieval).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Product")]
    public class ProductController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Retrieves a paged list of products.
        /// </summary>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Number of items per page.</param>
        /// <returns>
        /// 200 OK with a paged list of products when found.
        /// 400 Bad Request when the request parameters are invalid.
        /// 404 Not Found when no products exist for the specified page.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-all-products-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsPaged(int PageNumber,byte PageSize)
        {
            Result<PagedResult<GetProductQueryResponse>> result = await sender.Send(new GetAllProductPagedQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieves a paged list of products that match the specified name.
        /// </summary>
        /// <param name="Name">Product name or partial name to search for.</param>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Number of items per page.</param>
        /// <returns>
        /// 200 OK with matching products when any exist.
        /// 204 No Content when the search returns no matches.
        /// 400 Bad Request when the request parameters are invalid.
        /// 404 Not Found if the resource cannot be located.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-products-by-name-paged/{Name},{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByNamePaged(string Name, int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetProductQueryResponse>> result = await sender.Send(new GetAllProductPagedByNameQuery(Name, PageNumber, PageSize));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieves a single product by its identifier.
        /// </summary>
        /// <param name="productId">Unique identifier of the product.</param>
        /// <returns>
        /// 200 OK with the product when found.
        /// 400 Bad Request when the provided identifier is invalid.
        /// 404 Not Found when the product does not exist.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-product-by-id/{productId}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById(string productId)
        {
            Result<GetProductByIdQueryResponse> result = await sender.Send(new GetProductByIdQuery(productId));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieves paged reviews for a specific product.
        /// </summary>
        /// <param name="productId">Unique identifier of the product.</param>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Number of items per page.</param>
        /// <returns>
        /// 200 OK with product reviews when reviews exist.
        /// 204 No Content when there are no reviews for the specified product.
        /// 400 Bad Request when the request parameters are invalid.
        /// 404 Not Found when the product cannot be found.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-product-review-by-product-id/{productId},{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById(string productId,int PageNumber, byte PageSize)
        {
            Result<PagedResult<ProductReviewsResponse>> result = await sender.Send(new GetAllProductReviewsPagedQuery(productId,PageNumber,PageSize));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieves products by category in a paged fashion.
        /// </summary>
        /// <param name="category">Category identifier or name to filter products.</param>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Number of items per page.</param>
        /// <returns>
        /// 200 OK with matching products when found.
        /// 204 No Content when the category contains no products.
        /// 400 Bad Request when the request parameters are invalid.
        /// 404 Not Found when the category cannot be found.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-products-by-category/{category},{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByCategory(string category,int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetProductQueryResponse>> result = await sender.Send(new GetProductsByCategoryPagedQuery(category,PageNumber,PageSize));

            return result.ToActionResult();
        }

        /*[HttpGet("get-products-by-tag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByTag()
        {
            throw new NotImplementedException();
        }*/

        /* [HttpGet("get-top-rated-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopRatedProducts()
        {
            throw new NotImplementedException();
        }*/

        /* [HttpGet("get-products-by-age-paged")]
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
        }*/

        /*[HttpGet("get-products-with-all-filters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsWithAllFiltersPaged()
        {
            throw new NotImplementedException();
        }*/

        /// <summary>
        /// Retrieves the most loved products in a paged format.
        /// </summary>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Number of items per page.</param>
        /// <returns>
        /// 200 OK with a paged list of most loved products when available.
        /// 204 No Content when no most-loved products are found.
        /// 400 Bad Request when the request parameters are invalid.
        /// 404 Not Found when the resource cannot be located.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-most-loved-products/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostLoved(int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetProductQueryResponse>> result = await sender.Send(new GetAllMostLovedProductsPagedQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }

        /// <summary>
        /// Retrieves products filtered by gender in a paged fashion.
        /// </summary>
        /// <param name="Gender">Gender filter character (e.g. 'M', 'F', 'U').</param>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Number of items per page.</param>
        /// <returns>
        /// 200 OK with matching products when found.
        /// 204 No Content when the filter produces no results.
        /// 400 Bad Request when the request parameters are invalid.
        /// 404 Not Found when the resource cannot be located.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-products-by-gender-paged/{Gender},{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsByGenderPaged(char Gender, int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetProductQueryResponse>> result = await sender.Send(new GetAllProductPagedByGenderQuery(Gender, PageNumber, PageSize));

            return result.ToActionResult();
        }
    }
}
