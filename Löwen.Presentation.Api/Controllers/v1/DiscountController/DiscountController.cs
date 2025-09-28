using Löwen.Application.Features.DiscountFeature.Commands.AddDiscount;
using Löwen.Application.Features.DiscountFeature.Commands.AssignDiscountToProduct;
using Löwen.Application.Features.DiscountFeature.Commands.DeleteDiscount;
using Löwen.Application.Features.DiscountFeature.Commands.RemoveDiscountFromProduct;
using Löwen.Application.Features.DiscountFeature.Commands.UpdateDiscount;
using Löwen.Application.Features.DiscountFeature.Queries.GetAll;
using Löwen.Application.Features.DiscountFeature.Queries.GetById;
using Löwen.Application.Features.DiscountFeature.Queries.Response;
using Löwen.Presentation.Api.Controllers.v1.DiscountController.Models;

namespace Löwen.Presentation.Api.Controllers.v1.DiscountController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Discount")]
    public class DiscountController(ISender sender) : ControllerBase
    {
        [HttpPost("add-discount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDiscount([FromBody] AddDiscountModel model)
        {
            Result result = await sender.Send(new AddDiscountCommand(model.Name, model.DiscountType,
                                model.DiscountValue, model.StartDate, model.EndDate, model.IsActive));

            return result.ToActionResult();
        }

        [HttpPut("update-discount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscountModel model)
        {
            Result result = await sender.Send(new UpdateDiscountCommand(model.Id,model.Name, model.DiscountType,
                                 model.DiscountValue, model.StartDate, model.EndDate, model.IsActive));

            return result.ToActionResult();
        }

        [HttpDelete("remove-discount/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveDiscount(string Id)
        {
            Result result = await sender.Send(new RemoveDiscountCommand(Id));

            return result.ToActionResult();
        }

        [HttpGet("get-discount-by-id/{Id}")]
        [ProducesResponseType<DiscountResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDiscountById(string Id)
        {
            Result<DiscountResponse> result = await sender.Send(new GetDiscountByIdQuery(Id));

            return result.ToActionResult();
        }

        [HttpGet("get-all-discounts-paged/{PageNumber},{PageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDiscountsPaged(int PageNumber, byte PageSize)
        {
            Result<PagedResult<DiscountResponse>> result = await sender.Send(new GetAllDiscountQuery(PageNumber, PageSize));

            return result.ToActionResult();
        }

        [HttpPost("assign-discount-to-product/{discountId},{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignDiscountToProduct(string discountId, string productId)
        {
            Result result = await sender.Send(new AssignDiscountToProductCommand(discountId, productId));

            return result.ToActionResult();
        }

        [HttpDelete("remove-discount-from-product/{discountId},{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveDiscountFromProduct(string discountId, string productId)
        {
            Result result = await sender.Send(new RemoveDiscountFromProductCommand(discountId, productId));

            return result.ToActionResult();
        }

    }
}
