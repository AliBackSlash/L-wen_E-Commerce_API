using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CouponFeature.Commands.ApplyCouponToOrder;

internal class ApplyCouponToOrderCommandHandler(ICouponService couponService) : ICommandHandler<ApplyCouponToOrderCommand>
{
    public async Task<Result> Handle(ApplyCouponToOrderCommand command, CancellationToken ct)
    {
        var id = await couponService.GetIdIfCouponCodeFound(command.CouponCode,ct);

        if (id is null)
            return Result.Failure(Error.NotFound("Coupon.NotFound", $"Coupon with CouponCode {command.CouponCode} not found"));

        var applyResult = await couponService.ApplyCouponToOrder(id.Value, Guid.Parse(command.OrderId), ct);

        if (applyResult.IsFailure)
            return Result.Failure(applyResult.Errors);

        return Result.Success();
    }
}
