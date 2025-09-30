using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CouponFeature.Commands.RemoveCouponFromOrder;

internal class RemoveCouponFromOrderCommandHandler(ICouponService couponService) : ICommandHandler<RemoveCouponFromOrderCommand>
{
    public async Task<Result> Handle(RemoveCouponFromOrderCommand command, CancellationToken ct)
    {
        var id = await couponService.GetIdIfCouponCodeFound(command.CouponCode, ct);

        if (id is null)
            return Result.Failure(Error.NotFound("Coupon.NotFound", $"Coupon with CouponCode {command.CouponCode} not found"));

        var removeResult = await couponService.RemoveCouponFromOrder(id.Value,Guid.Parse(command.OrderId), ct);

        if (removeResult.IsFailure)
            return Result.Failure(removeResult.Errors);

        return Result.Success();
    }
}
