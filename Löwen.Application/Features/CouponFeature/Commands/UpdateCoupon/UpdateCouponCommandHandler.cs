using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CouponFeature.Commands.UpdateCoupon;

internal class UpdateCouponCommandHandler(ICouponService couponService) : ICommandHandler<UpdateCouponCommand>
{
    public async Task<Result> Handle(UpdateCouponCommand command, CancellationToken ct)
    {
        var coupon = await couponService.GetByIdAsync(Guid.Parse(command.Id), ct);
        if (coupon is null)
            return Result.Failure(Error.NotFound("Coupon.NotFound", $"Coupon with Id {command.Id} not found"));

        coupon.DiscountType = command.DiscountType ?? coupon.DiscountType;
        coupon.DiscountValue = command.DiscountValue ?? coupon.DiscountValue;
        coupon.Code = command.Code ?? coupon.Code;
        coupon.StartDate = command.StartDate ?? coupon.StartDate;
        coupon.EndDate = command.EndDate ?? coupon.EndDate;
        coupon.IsActive = command.IsActive ?? coupon.IsActive;
        coupon.UsageLimit = command.UsageLimit ?? coupon.UsageLimit;

        var updateResult = await couponService.UpdateAsync(coupon, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
