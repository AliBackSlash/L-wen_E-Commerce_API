
using Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;
using Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProduct;
using Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;
using Löwen.Application.Features.AdminFeature.Commands.Tag.AddTag;
using Löwen.Application.Features.AdminFeature.Commands.Tag.RemoveTag;
using Löwen.Application.Features.AdminFeature.Commands.Tag.UpdateTag;
using Löwen.Presentation.Api.Controllers.v1.AdminController.Models;
using MediatR;
using System.Xml.Linq;

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
            Result result = await sender.Send(new AddCategoryCommand(request.Category, request.Gender, request.AgeFrom, request.AgeTo));

            return result.ToActionResult();
        }

        [HttpPut("update-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryModel request)
        {
            Result result = await sender.Send(new UpdateCategoryCommand(request.Id,request.Category, request.Gender, request.AgeFrom, request.AgeTo));

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
            Result<Guid> result = await sender.Send(new AddProductCommand(model.Name, model.Description, model.Price, model.StockQuantity, model.Status, model.CategoryId));

            return result.ToActionResult();
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

        [HttpGet("get-user-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-user-by-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByEmail()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-users-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersPaged()
        {
            throw new NotImplementedException();
        }

        [HttpPost("Deactivate-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeactivateUser()
        {
            throw new NotImplementedException();
        }

        [HttpPost("activate-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateUser()
        {
            throw new NotImplementedException();
        }

      

    }
}
