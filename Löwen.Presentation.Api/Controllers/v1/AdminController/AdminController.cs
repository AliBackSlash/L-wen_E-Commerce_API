namespace Löwen.Presentation.Api.Controllers.v1.AdminController
{
    /// <summary>
    /// Admin API endpoints for managing categories, products, users and related resources.
    /// Produces both JSON and XML so OpenAPI/Swagger can expose XML-formatted responses when requested.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Admin")]
    [Produces("application/json", "application/xml")]
    [Authorize(Roles ="Admin")]
    public class AdminController(ISender sender, IFileService fileService) : ControllerBase
    {
        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="request">Category details to add.</param>
        /// <returns>
        /// 201 Created when the category is created successfully.
        /// 400 Bad Request for validation errors.
        /// 409 Conflict when a duplicate category exists.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("add-category")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryModel request)
        {
            Result result = await sender.Send(new AddCategoryCommand(request.Category, request.Gender));
            return result.ToActionResult();
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="request">Updated category data.</param>
        /// <returns>
        /// 200 OK when the category is updated successfully.
        /// 400 Bad Request for validation errors.
        /// 404 Not Found when the target category does not exist.
        /// 409 Conflict when an update would cause a duplicate or conflict.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("update-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryModel request)
        {
            Result result = await sender.Send(new UpdateCategoryCommand(request.Id, request.Category, request.Gender));
            return result.ToActionResult();
        }

        /// <summary>
        /// Removes a category by id.
        /// </summary>
        /// <param name="Id">Category id.</param>
        /// <returns>
        /// 204 No Content when the category is removed successfully.
        /// 404 Not Found when the category does not exist.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpDelete("remove-category/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveCategory(Guid Id)
        {
            Result result = await sender.Send(new RemoveCategoryCommand(Id));

            return result.ToActionResult();
        }

        /*[HttpPost("add-tag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTag([FromBody] AddTagModel request)
        {
            Result result = await sender.Send(new AddTagCommand(request.Tag, request.productId));

            return result.ToActionResult();
        }*/
        /*
        [HttpPut("update-tag/{productId},{tagName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTag(string productId, string tagName)
        {
            Result result = await sender.Send(new UpdateTagCommand(productId, tagName));

            return result.ToActionResult();
        }

        */
        /*[HttpDelete("remove-tag/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveTag(Guid Id)
        {
            Result result = await sender.Send(new RemoveTagCommand(Id));

            return result.ToActionResult();
        }*/

        /// <summary>
        /// Returns a paged list of products.
        /// </summary>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Page size.</param>
        /// <returns>
        /// 200 OK with a paged result when products exist.
        /// 204 No Content when no products are found for the given page.
        /// 400 Bad Request for invalid parameters.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-products-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<GetProductsQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsPaged(int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetProductsQueryResponse>> result = await sender.Send(new GetProductsQuery(PageNumber, PageSize));
            return result.ToActionResult();
        }

        /// <summary>
        /// Returns a paged list of products filtered by Id or Name.
        /// </summary>
        /// <param name="IdOrName">Id or Name filter.</param>
        /// <param name="PageNumber">Page number (1-based).</param>
        /// <param name="PageSize">Page size.</param>
        /// <returns>
        /// 200 OK with a paged result when matching products exist.
        /// 204 No Content when there are no matches.
        /// 400 Bad Request for invalid inputs.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-products-paged-filter-by-id-or-name/{IdOrName},{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<GetProductsQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProductsPagedByIdOrName(string IdOrName, int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetProductsQueryResponse>> result = await sender.Send(new GetProductsByIdOrNameQuery(IdOrName, PageNumber, PageSize));
            return result.ToActionResult();
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="model">Product model to create.</param>
        /// <returns>
        /// 201 Created containing the created product information when successful.
        /// 400 Bad Request for validation errors.
        /// 401 Unauthorized when token is missing or invalid.
        /// 409 Conflict when a product with the same identity already exists.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("add-product")]
        [ProducesResponseType(typeof(AddProductCommandResponse), StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<Error>(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProduct([FromBody] AddProductModel model)
        {
            var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(createdBy))
                return Result.Failure(new Error("api/Admin/add-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result<AddProductCommandResponse> result = await sender.Send(new AddProductCommand(model.Name, model.Description, model.Status, model.CategoryId,
                createdBy, model.Tags, model.MainPrice, model.VariantDtos));

            return result.ToActionResult();
        }

        /// <summary>
        /// Uploads product images and associates them with the given product.
        /// </summary>
        /// <param name="ProductId">Target product id.</param>
        /// <param name="model">Form data containing files to upload.</param>
        /// <returns>
        /// 201 Created when images are uploaded and associated successfully.
        /// 400 Bad Request for invalid input.
        /// 409 Conflict when uploaded images conflict with existing resources.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("upload-product-images/{ProductId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadProductImages(Guid ProductId, [FromForm] UploudPruductImagesModel model)
        {
            var UploudResult = await fileService.UploadProoductImagesAsync(model.Uplouds);
            if (UploudResult.IsFailure)
                return UploudResult.ToActionResult();

            List<AddProductImagesDto> images
                = new();

            foreach (var i in UploudResult.Value)
            {
                images.Add(new AddProductImagesDto
                {
                    ProductId = ProductId,
                    Path = i.Path,
                    IsMain = i.IsMain,
                });
            }

            Result result = await sender.Send(new AddProductImagesCommand(images));

            if (result.IsFailure)
                fileService.DeleteFiles(UploudResult.Value.Select(x => x.Path));

            return result.ToActionResult();
        }

        /// <summary>
        /// Removes a product image by path.
        /// </summary>
        /// <param name="imagePath">Path of the image to remove.</param>
        /// <returns>
        /// 204 No Content when the image is deleted successfully.
        /// 404 Not Found when the image is not found.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpDelete("remove-product-image/{imagePath}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProductImage(string imagePath)
        {
            Result result = await sender.Send(new DeleteProductImagesCommand(imagePath));

            if (result.IsSuccess)
                fileService.DeleteFile(imagePath);

            return result.ToActionResult();
        }

        /// <summary>
        /// Updates product metadata.
        /// </summary>
        /// <param name="model">Updated product fields.</param>
        /// <returns>
        /// 200 OK when the product metadata is updated successfully.
        /// 400 Bad Request for validation errors.
        /// 404 Not Found when the product does not exist.
        /// 409 Conflict when the update would cause a conflict.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductModel model)
        {
            Result result = await sender.Send(new UpdateProductCommand(model.Id, model.Name, model.Description, model.MainPrice, model.Status, model.CategoryId));
            return result.ToActionResult();
        }

        /// <summary>
        /// Adds a product variant.
        /// </summary>
        /// <param name="model">Variant details to add.</param>
        /// <returns>
        /// 201 Created when the variant is added successfully.
        /// 400 Bad Request for invalid inputs.
        /// 409 Conflict when a similar variant already exists.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPost("add-product-variant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProductVariant([FromBody] AddProductVariantModel model)
        {
            Result result = await sender.Send(new AddProductVariantCommand(model.ProductId, model.ColorId, model.SizeId, model.Price, model.StockQuantity));
            return result.ToActionResult();
        }

        /// <summary>
        /// Updates a product variant.
        /// </summary>
        /// <param name="model">Variant update details.</param>
        /// <returns>
        /// 200 OK when the variant is updated successfully.
        /// 400 Bad Request for validation errors.
        /// 404 Not Found when the variant does not exist.
        /// 409 Conflict when the update causes a conflict.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("update-product-variant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductVariant([FromBody] UpdateProductVariantModel model)
        {
            Result result = await sender.Send(new UpdateProductVariantCommand(model.Id, model.ColorId, model.SizeId, model.Price, model.StockQuantity));
            return result.ToActionResult();
        }

        /// <summary>
        /// Removes a product variant.
        /// </summary>
        /// <param name="Id">Variant identifier.</param>
        /// <returns>
        /// 204 No Content when the variant is removed successfully.
        /// 404 Not Found when the variant does not exist.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpDelete("remove-product-variant/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProductVariant(string Id)
        {
            Result result = await sender.Send(new RemoveProductVariantCommand(Id));
            return result.ToActionResult();
        }

        /// <summary>
        /// Removes a product.
        /// </summary>
        /// <param name="Id">Product identifier.</param>
        /// <returns>
        /// 204 No Content when the product is removed successfully.
        /// 404 Not Found when the product does not exist.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpDelete("remove-product/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProduct(string Id)
        {
            Result result = await sender.Send(new RemoveProductCommand(Id));
            return result.ToActionResult();
        }

        /// <summary>
        /// Assigns orders to delivery.
        /// </summary>
        /// <param name="model">Collection of delivery-order mappings.</param>
        /// <returns>
        /// 200 OK when orders are assigned to delivery successfully.
        /// 400 Bad Request for invalid payload.
        /// 404 Not Found when referenced orders or deliveries cannot be found.
        /// 409 Conflict when assignments conflict with existing state.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("assigned-orders-to-delivery")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignedOrdersToDelivery([FromBody] AssignedOrdersToDeliveryModel model)
        {
            Result result = await sender.Send(new AssignedOrdersToDeliveryCommand(model.deliveryOrders));
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a user by id.
        /// </summary>
        /// <param name="Id">User id.</param>
        /// <returns>
        /// 200 OK with user information when found.
        /// 404 Not Found when the user does not exist.
        /// 400 Bad Request for invalid id format.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-user-by-id/{Id}")]
        [ProducesResponseType(typeof(GetUserByIdQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(string Id)
        {
            Result<GetUserByIdQueryResponse> result = await sender.Send(new GetUserByIdQuery(Id));
            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a user by email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>
        /// 200 OK with user information when found.
        /// 404 Not Found when the user does not exist.
        /// 400 Bad Request for invalid email format.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-user-by-email/{email}")]
        [ProducesResponseType(typeof(GetUserByEmailQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            Result<GetUserByEmailQueryResponse> result = await sender.Send(new GetUserByEmailQuery(email));
            return result.ToActionResult();
        }

        /// <summary>
        /// Returns a paged list of users.
        /// </summary>
        /// <param name="PageNumber">Page number.</param>
        /// <param name="PageSize">Page size.</param>
        /// <returns>
        /// 200 OK with a paged result when users exist.
        /// 204 No Content when there are no users for the given page.
        /// 400 Bad Request for invalid paging parameters.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpGet("get-users-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(typeof(PagedResult<GetUsersQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersPaged(int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetUsersQueryResponse>> result = await sender.Send(new GetUsersQuery(PageNumber, PageSize));
            return result.ToActionResult();
        }

        /// <summary>
        /// Marks a user as deleted (soft delete).
        /// </summary>
        /// <param name="Id">User id to mark as deleted.</param>
        /// <returns>
        /// 200 OK when the user is marked as deleted successfully.
        /// 400 Bad Request for invalid request.
        /// 404 Not Found when the user is not found.
        /// 409 Conflict when the operation conflicts with current state.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("mark-user-as-deleted/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeactivateUser(Guid Id)
        {
            Result result = await sender.Send(new MarkUserAsDeletedCommand(Id));
            return result.ToActionResult();
        }

        /// <summary>
        /// Activates a previously marked-as-deleted user.
        /// </summary>
        /// <param name="Id">User id to activate.</param>
        /// <returns>
        /// 200 OK when the user is activated successfully.
        /// 400 Bad Request for invalid request.
        /// 404 Not Found when the user is not found.
        /// 409 Conflict when the operation conflicts with current state.
        /// 500 Internal Server Error for unexpected failures.
        /// </returns>
        [HttpPut("activate-marked-user-as-deleted/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status409Conflict)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateUser(Guid Id)
        {
            Result result = await sender.Send(new ActivateMarkedUserAsDeletedCommand(Id));
            return result.ToActionResult();
        }
    }
}