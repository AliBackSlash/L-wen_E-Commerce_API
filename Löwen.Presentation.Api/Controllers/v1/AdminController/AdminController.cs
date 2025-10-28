

namespace Löwen.Presentation.Api.Controllers.v1.AdminController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Admin")]
    public class AdminController(ISender sender, IFileService fileService) : ControllerBase
    {
        [HttpPost("add-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryModel request)
        {

            Result result = await sender.Send(new AddCategoryCommand(request.Category, request.Gender));

            return result.ToActionResult();
        }

        [HttpPut("update-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryModel request)
        {
            Result result = await sender.Send(new UpdateCategoryCommand(request.Id,request.Category, request.Gender));

            return result.ToActionResult();
        }

        [HttpDelete("remove-category/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
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

        *//*[HttpDelete("remove-tag/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveTag(Guid Id)
        {
            Result result = await sender.Send(new RemoveTagCommand(Id));

            return result.ToActionResult();
        }*/

        [HttpPost("add-product")]
        [ProducesResponseType<Result<Guid>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProduct([FromBody] AddProductModel model)
        {
            var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(createdBy))
                return Result.Failure(new Error("api/Admin/add-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result<AddProductCommandResponse> result = await sender.Send(new AddProductCommand(model.Name, model.Description,model.Status, model.CategoryId, 
                createdBy ,model.Tags,model.MainPrice, model.VariantDtos));

            return result.ToActionResult();
        }
       
        [HttpPost("upload-product-images/{ProductId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadProductImages(Guid ProductId, [FromForm] UploudPruductImagesModel model)
        {
           /* if (model.Uplouds.Count(x => x.IsMain) != 1)
                return Result.Failure(new Error("Admin.UploadProductImages", 
                    "You should select one image for product to be main image.", ErrorType.Conflict)).ToActionResult();
*/
            var UploudResult = await fileService.UploadProoductImagesAsync(model.Uplouds);
            if (UploudResult.IsFailure)
               return UploudResult.ToActionResult();

            List<AddProductImagesDto> images
                = new ();

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

        [HttpDelete("remove-product-image/{imagePath}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProductImage(string imagePath)
        {
            Result result = await sender.Send(new DeleteProductImagesCommand(imagePath));
          
            if (result.IsSuccess)
                fileService.DeleteFile(imagePath);

            return result.ToActionResult();
        }

        [HttpPut("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductModel model)
        {
            Result result = await sender.Send(new UpdateProductCommand(model.Id,model.Name, model.Description,model.MainPrice, model.Status, model.CategoryId));

            return result.ToActionResult();
        }

        [HttpPost("add-product-variant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProductVariant([FromBody] AddProductVariantModel model)
        {
            Result result = await sender.Send(new AddProductVariantCommand(model.ProductId,model.ColorId,model.SizeId,model.Price,model.StockQuantity));

            return result.ToActionResult();
        }

        [HttpPut("update-product-variant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductVariant([FromBody] UpdateProductVariantModel model)
        {
            Result result = await sender.Send(new UpdateProductVariantCommand(model.Id,model.ColorId,model.SizeId,model.Price,model.StockQuantity));

            return result.ToActionResult();
        }

        [HttpDelete("remove-product-variant/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProductVariant(string Id)
        {
            Result result = await sender.Send(new RemoveProductVariantCommand(Id));

            return result.ToActionResult();
        }

        [HttpDelete("remove-product/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProduct(string Id)
        {
            Result result = await sender.Send(new RemoveProductCommand(Id));

            return result.ToActionResult();
        }

        [HttpPut("assigned-orders-to-delivery")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignedOrdersToDelivery([FromBody] AssignedOrdersToDeliveryModel model)
        {
            Result result = await sender.Send(new AssignedOrdersToDeliveryCommand(model.deliveryOrders));

            return result.ToActionResult();
        }

        [HttpGet("get-user-by-id/{Id}")]
        [ProducesResponseType<GetUserByIdQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(string Id)
        {
            Result<GetUserByIdQueryResponse> result = await sender.Send(new GetUserByIdQuery(Id));

            return result.ToActionResult();
        }

        [HttpGet("get-user-by-email/{email}")]
        [ProducesResponseType<GetUserByEmailQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            Result<GetUserByEmailQueryResponse> result = await sender.Send(new GetUserByEmailQuery(email));

            return result.ToActionResult();
        }

        [HttpGet("get-users-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType<PagedResult<GetUsersQueryResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersPaged(int PageNumber, byte PageSize)
        {
            Result<PagedResult<GetUsersQueryResponse>> result = await sender.Send(new GetUsersQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }

        [HttpPut("mark-user-as-deleted/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeactivateUser(Guid Id)
        {
            Result result = await sender.Send(new MarkUserAsDeletedCommand(Id));

            return result.ToActionResult();
        }

        [HttpPut("activate-marked-user-as-deleted/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateUser(Guid Id)
        {
            Result result = await sender.Send(new ActivateMarkedUserAsDeletedCommand(Id));

            return result.ToActionResult();
        }

      

    }
}
