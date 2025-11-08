using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CouponFeature.Commands.RemoveCoupon;

internal class RemoveCouponCommandHandler(ICouponService couponService) : ICommandHandler<RemoveCouponCommand>
{
    public async Task<Result> Handle(RemoveCouponCommand command, CancellationToken ct)
    {
        var coupon = await couponService.GetByIdAsync(Guid.Parse(command.CouponId), ct);
        if (coupon is null)
            return Result.Failure(Error.NotFound("Coupon.NotFound", $"Coupon with Id {command.CouponId} not found"));

        var removeResult = await couponService.DeleteAsync(coupon, ct);

        if (removeResult.IsFailure)
            return Result.Failure(removeResult.Errors);

        return Result.Success();
    }
}
