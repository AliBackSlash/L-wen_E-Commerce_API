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
        /// <remarks>
        /// This endpoint allows administrators to create a new category in the system.
        /// Use this action when you need to add a new product category with associated gender information.
        /// The category must not already exist to avoid conflicts.
        /// </remarks>
        /// <param name="request">Category details to add, provided in the request body as JSON or XML. Contains the category name and gender classification.</param>
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
        /// <remarks>
        /// This endpoint modifies the details of an existing category, including its name and gender classification.
        /// Use this action to rename a category, change its gender association, or update other category attributes.
        /// The category must exist before it can be updated.
        /// </remarks>
        /// <param name="request">Updated category data provided in the request body as JSON or XML. Must include the category ID and new values for category name and gender.</param>
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
        /// <remarks>
        /// This endpoint permanently deletes a category from the system.
        /// Use this action when you need to remove a category that is no longer needed.
        /// Ensure that no products are associated with this category before deletion, as this may violate data integrity constraints.
        /// </remarks>
        /// <param name="Id">Category identifier (GUID) provided as a route parameter. This must be a valid GUID format.</param>
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
        /// <remarks>
        /// This endpoint retrieves a paginated list of all products in the system without any filtering.
        /// Use this action to browse products with pagination support for performance optimization.
        /// The response includes product metadata but not detailed variant information.
        /// Useful for bulk operations and inventory management views.
        /// </remarks>
        /// <param name="PageNumber">Page number for pagination (1-based indexing) provided as a route parameter. Must be a positive integer greater than or equal to 1.</param>
        /// <param name="PageSize">Number of products to return per page provided as a route parameter. Must be a valid byte value (1-255).</param>
        /// <returns>
        /// 200 OK with a paged result containing products when products exist for the requested page.
        /// 204 No Content when no products are found for the given page number.
        /// 400 Bad Request for invalid parameters (e.g., invalid page number or size).
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
        /// <remarks>
        /// This endpoint retrieves a paginated list of products that match the provided search criteria.
        /// Use this action to search for specific products by their unique identifier or display name.
        /// The search is case-insensitive and matches partial strings in product names.
        /// Useful for product lookups, search functionality, and inventory searches.
        /// </remarks>
        /// <param name="IdOrName">Search filter for product ID or name provided as a route parameter. Can be a product identifier or partial/full product name.</param>
        /// <param name="PageNumber">Page number for pagination (1-based indexing) provided as a route parameter. Must be a positive integer greater than or equal to 1.</param>
        /// <param name="PageSize">Number of products to return per page provided as a route parameter. Must be a valid byte value (1-255).</param>
        /// <returns>
        /// 200 OK with a paged result containing matching products when results are found.
        /// 204 No Content when there are no matches for the search criteria.
        /// 400 Bad Request for invalid inputs (e.g., empty search string or invalid paging parameters).
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
        /// <remarks>
        /// This endpoint creates a new product in the system with comprehensive details including metadata, pricing, variants, and tags.
        /// Use this action when you need to introduce a new product to the catalog.
        /// The authenticated user's identity is captured as the creator of the product.
        /// All required fields must be provided, and the product name must be unique within the system.
        /// Product variants and tags can be specified during creation or added later through separate endpoints.
        /// </remarks>
        /// <param name="model">Product model containing comprehensive product details provided in the request body as JSON or XML. Includes name, description, category, status, pricing, variants, and tags.</param>
        /// <returns>
        /// 201 Created containing the created product information (including the generated product ID) when successful.
        /// 400 Bad Request for validation errors (e.g., missing required fields, invalid category).
        /// 401 Unauthorized when the authentication token is missing or invalid, preventing user identification.
        /// 409 Conflict when a product with the same name or identity already exists.
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
        /// <remarks>
        /// This endpoint handles image file uploads for a specific product and associates them with the product record.
        /// Use this action to add visual content to products, including main/thumbnail images and additional product photos.
        /// Images are stored on the file system, and references are maintained in the database.
        /// If database association fails after successful uploads, uploaded files are automatically cleaned up to prevent orphaned files.
        /// Supports multiple image uploads in a single request.
        /// </remarks>
        /// <param name="ProductId">Target product identifier (GUID) provided as a route parameter. This must be a valid GUID referencing an existing product.</param>
        /// <param name="model">Form data containing files to upload, provided as multipart/form-data in the request body. Each file is a product image that will be associated with the specified product.</param>
        /// <returns>
        /// 201 Created when images are uploaded and associated successfully with the product.
        /// 400 Bad Request for invalid input (e.g., invalid file format, product ID, or no files provided).
        /// 409 Conflict when uploaded images conflict with existing resources or database constraints.
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
        /// <remarks>
        /// This endpoint deletes a specific product image from the system.
        /// Use this action when you need to remove outdated, incorrect, or unwanted product images.
        /// The image is removed from both the database and the file system upon successful deletion.
        /// Ensure that at least one image remains for the product if image coverage is required by business rules.
        /// </remarks>
        /// <param name="imagePath">Path of the image to remove provided as a route parameter. Must be the full or relative file path of the image to delete.</param>
        /// <returns>
        /// 204 No Content when the image is deleted successfully from the database and file system.
        /// 404 Not Found when the image with the specified path is not found in the system.
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
        /// <remarks>
        /// This endpoint modifies the core metadata of an existing product, such as name, description, pricing, status, and category assignment.
        /// Use this action to make updates to product information without modifying variants or inventory.
        /// The product must exist before it can be updated.
        /// This operation does not affect associated images, variants, or tags; use dedicated endpoints to manage those resources.
        /// </remarks>
        /// <param name="model">Updated product fields provided in the request body as JSON or XML. Must include the product ID and fields to be updated (name, description, price, status, category).</param>
        /// <returns>
        /// 200 OK when the product metadata is updated successfully.
        /// 400 Bad Request for validation errors (e.g., invalid category, invalid price).
        /// 404 Not Found when the product does not exist.
        /// 409 Conflict when the update would cause a conflict (e.g., duplicate product name).
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
        /// <remarks>
        /// This endpoint creates a new variant for an existing product with specific attributes (color, size) and pricing/stock information.
        /// Use this action when you need to add different variations of a product, such as different sizes or colors with separate pricing and inventory.
        /// Each variant combination (product + color + size) must be unique within the system.
        /// Variants enable flexible product management for items with multiple options.
        /// </remarks>
        /// <param name="model">Variant details to add provided in the request body as JSON or XML. Includes product ID, color ID, size ID, unit price, and stock quantity.</param>
        /// <returns>
        /// 201 Created when the variant is added successfully.
        /// 400 Bad Request for invalid inputs (e.g., invalid product ID, missing color information, invalid price).
        /// 409 Conflict when a similar variant already exists for the product with the same color/size combination.
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
        /// <remarks>
        /// This endpoint modifies the properties of an existing product variant, including color, size, pricing, and stock quantity.
        /// Use this action to adjust variant details such as price changes, inventory adjustments, or attribute updates.
        /// The variant must exist before it can be updated.
        /// Only non-null fields in the request will be updated; null values are ignored.
        /// </remarks>
        /// <param name="model">Variant update details provided in the request body as JSON or XML. Must include the variant ID and any fields to be updated.</param>
        /// <returns>
        /// 200 OK when the variant is updated successfully.
        /// 400 Bad Request for validation errors (e.g., invalid color/size reference, invalid price).
        /// 404 Not Found when the variant does not exist.
        /// 409 Conflict when the update causes a conflict (e.g., duplicate color/size combination).
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
        /// <remarks>
        /// This endpoint deletes a specific product variant from the system.
        /// Use this action when you need to discontinue a variant or remove invalid/duplicate variants.
        /// Removal only affects the specific variant; other variants of the same product remain unaffected.
        /// Ensure inventory adjustments are made before removing variants if necessary.
        /// </remarks>
        /// <param name="Id">Variant identifier provided as a route parameter. Must be a valid variant ID referencing an existing product variant.</param>
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
        /// <remarks>
        /// This endpoint permanently deletes a product from the system.
        /// Use this action when you need to remove products that are no longer sold or are discontinued.
        /// Deletion cascades to remove all associated data including variants, images, and inventory records.
        /// Ensure that no active orders reference this product before deletion to maintain data consistency.
        /// </remarks>
        /// <param name="Id">Product identifier provided as a route parameter. Must be a valid product ID referencing an existing product.</param>
        /// <returns>
        /// 204 No Content when the product is removed successfully along with all its associated data.
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
        /// <remarks>
        /// This endpoint assigns one or more orders to a delivery operation.
        /// Use this action during the fulfillment process to associate orders with delivery routes or delivery agents.
        /// Multiple orders can be assigned to a single delivery in a batch operation.
        /// Orders must exist and be in a state eligible for delivery assignment before this operation.
        /// </remarks>
        /// <param name="model">Collection of delivery-order mappings provided in the request body as JSON or XML. Each mapping specifies a delivery ID and the order ID(s) to assign to it.</param>
        /// <returns>
        /// 200 OK when orders are assigned to delivery successfully.
        /// 400 Bad Request for invalid payload (e.g., missing delivery ID or order ID).
        /// 404 Not Found when referenced orders or deliveries cannot be found.
        /// 409 Conflict when assignments conflict with existing state (e.g., order already assigned to another delivery).
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
        /// <remarks>
        /// This endpoint retrieves detailed information about a specific user in the system.
        /// Use this action to fetch user profile data, contact information, and account status.
        /// The user ID must exist in the system for successful retrieval.
        /// This operation is useful for user management, account verification, and administrative tasks.
        /// </remarks>
        /// <param name="Id">User identifier provided as a route parameter. Can be a user ID (string or GUID) that uniquely identifies the user in the system.</param>
        /// <returns>
        /// 200 OK with user information when found. Contains complete user profile data.
        /// 404 Not Found when the user does not exist or has been deleted.
        /// 400 Bad Request for invalid id format (e.g., malformed GUID or invalid string format).
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
        /// <remarks>
        /// This endpoint retrieves detailed information about a user using their email address.
        /// Use this action to look up users by their contact email, which is typically unique across the system.
        /// The email address must be in a valid format and must exist in the system for successful retrieval.
        /// This operation is useful for user verification, account lookup, and customer service scenarios.
        /// </remarks>
        /// <param name="email">User email address provided as a route parameter. Must be a valid email format that exists in the system.</param>
        /// <returns>
        /// 200 OK with user information when found. Contains complete user profile data for the specified email.
        /// 404 Not Found when the user with the specified email does not exist.
        /// 400 Bad Request for invalid email format (e.g., missing @ symbol, malformed domain).
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
        /// <remarks>
        /// This endpoint retrieves a paginated list of all users in the system.
        /// Use this action for user management, administrative dashboards, and bulk user operations.
        /// The response includes user profile information with pagination support for performance optimization.
        /// Results include both active and inactive (soft-deleted) users depending on system configuration.
        /// </remarks>
        /// <param name="PageNumber">Page number for pagination (1-based indexing) provided as a route parameter. Must be a positive integer greater than or equal to 1.</param>
        /// <param name="PageSize">Number of users to return per page provided as a route parameter. Must be a valid byte value (1-255) representing the maximum users per page.</param>
        /// <returns>
        /// 200 OK with a paged result containing user records when users exist for the requested page.
        /// 204 No Content when there are no users for the given page number.
        /// 400 Bad Request for invalid paging parameters (e.g., page number less than 1, page size of 0).
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
        /// <remarks>
        /// This endpoint performs a soft delete on a user account, marking them as inactive without permanently removing data.
        /// Use this action to deactivate user accounts while preserving historical data, order records, and audit trails.
        /// Soft-deleted users are typically hidden from normal user listings but remain retrievable for audit purposes.
        /// This is the preferred method for user deactivation in compliance with data retention policies.
        /// The user must exist and not already be marked as deleted for this operation to succeed.
        /// </remarks>
        /// <param name="Id">User identifier (GUID) provided as a route parameter. Must be a valid GUID referencing an existing user in the system.</param>
        /// <returns>
        /// 200 OK when the user is marked as deleted successfully.
        /// 400 Bad Request for invalid request (e.g., invalid GUID format).
        /// 404 Not Found when the user is not found.
        /// 409 Conflict when the operation conflicts with current state (e.g., user is already marked as deleted).
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
        /// <remarks>
        /// This endpoint restores a soft-deleted user account, making them active and visible in the system again.
        /// Use this action to reactivate deactivated user accounts or restore accounts marked for deletion by mistake.
        /// Only users that are currently marked as deleted can be activated through this operation.
        /// Activation restores full user access and visibility without requiring re-registration or credential reset.
        /// </remarks>
        /// <param name="Id">User identifier (GUID) provided as a route parameter. Must be a valid GUID referencing an existing soft-deleted user in the system.</param>
        /// <returns>
        /// 200 OK when the user is activated successfully.
        /// 400 Bad Request for invalid request (e.g., invalid GUID format).
        /// 404 Not Found when the user is not found.
        /// 409 Conflict when the operation conflicts with current state (e.g., user is already active, not marked as deleted).
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