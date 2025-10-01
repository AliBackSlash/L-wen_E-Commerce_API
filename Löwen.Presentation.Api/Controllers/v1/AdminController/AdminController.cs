using Löwen.Application.Features.OrderFeature.Commands.AssignedOrdersToDelivery;
using Löwen.Presentation.Api.Controllers.v1.AdminController.Models.DeliveryOrder;

namespace Löwen.Presentation.Api.Controllers.v1.AdminController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Admin")]
    public class AdminController(ISender sender) : ControllerBase
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

        [HttpPost("add-tag")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTag([FromBody] AddTagModel request)
        {
            Result result = await sender.Send(new AddTagCommand(request.Tag, request.productId));

            return result.ToActionResult();
        }

        [HttpPut("update-tag/{Id},{tagName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTag(string Id,string tagName)
        {
            Result result = await sender.Send(new UpdateTagCommand(Id,tagName));

            return result.ToActionResult();
        }

        [HttpDelete("remove-tag/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveTag(Guid Id)
        {
            Result result = await sender.Send(new RemoveTagCommand(Id));

            return result.ToActionResult();
        }

        [HttpPost("add-product")]
        [ProducesResponseType<Result<Guid>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProduct([FromBody] AddProductModel model)
        {
            var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(createdBy))
                return Result.Failure(new Error("api/Admin/add-product", "Valid token is required", ErrorType.Unauthorized)).ToActionResult();

            Result<Guid> result = await sender.Send(new AddProductCommand(model.Name, model.Description, model.Price, model.StockQuantity, model.Status, model.CategoryId, createdBy));

            return result.ToActionResult();
        }
       
        [HttpPost("upload-product-images")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadProductImages()
        {
            throw new NotImplementedException();
        }

        [HttpPut("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductModel model)
        {
            Result result = await sender.Send(new UpdateProductCommand(model.Id,model.Name, model.Description, model.Price, model.StockQuantity, model.Status, model.CategoryId));

            return result.ToActionResult();
        }

        [HttpDelete("remove-product/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProduct(Guid Id)
        {
            Result result = await sender.Send(new RemoveProductCommand(Id));

            return result.ToActionResult();
        }

        [HttpDelete("assigned-orders-to-delivery")]
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
