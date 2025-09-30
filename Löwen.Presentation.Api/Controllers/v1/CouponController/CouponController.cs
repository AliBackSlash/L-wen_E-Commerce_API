using Löwen.Application.Features.CouponFeature.Commands.AddCoupon;
using Löwen.Application.Features.CouponFeature.Commands.ApplyCouponToOrder;
using Löwen.Application.Features.CouponFeature.Commands.RemoveCoupon;
using Löwen.Application.Features.CouponFeature.Commands.RemoveCouponFromOrder;
using Löwen.Application.Features.CouponFeature.Commands.UpdateCoupon;
using Löwen.Domain.Entities;
using Löwen.Presentation.Api.Controllers.v1.CouponController.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Löwen.Presentation.Api.Controllers.v1.CouponController
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Coupon")]
    public class CouponController(ISender sender) : ControllerBase
    {
        [HttpPost("add-coupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCoupon([FromBody] AddCouponModel model)
        {
            Result result = await sender.Send(new AddCouponCommand(model.Code,model.DiscountType,
                                                 model.DiscountValue, model.StartDate, model.EndDate, model.IsActive, model.UsageLimit));

            return result.ToActionResult();
        }

        [HttpPut("update-coupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCoupon([FromBody] UpdateCouponModel model)
        {
            Result result = await sender.Send(new UpdateCouponCommand(model.CouponId,model.Code, model.DiscountType,
                                                 model.DiscountValue, model.StartDate, model.EndDate, model.IsActive, model.UsageLimit));

            return result.ToActionResult();
        }

        [HttpDelete("remove-coupon/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveCoupon(string Id)
        {
            Result result = await sender.Send(new RemoveCouponCommand(Id));

            return result.ToActionResult();
        }

        [HttpGet("get-coupon-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCouponById()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-coupon-by-code")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCouponByCode()
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-all-coupons-paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCouponsPaged()
        {
            throw new NotImplementedException();
        }

        [HttpPost("apply-coupon-to-order/{CouponCode},{OrderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApplyCouponToOrder(string CouponCode,string OrderId)
        {
            Result result = await sender.Send(new ApplyCouponToOrderCommand(CouponCode, OrderId));

            return result.ToActionResult();
        }

        [HttpDelete("remove-coupon-from-order/{CouponCode},{OrderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<IEnumerable<Error>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveCouponFromOrder(string CouponCode, string OrderId)
        {
            Result result = await sender.Send(new RemoveCouponFromOrderCommand(CouponCode, OrderId));

            return result.ToActionResult();
        }

    }
}
