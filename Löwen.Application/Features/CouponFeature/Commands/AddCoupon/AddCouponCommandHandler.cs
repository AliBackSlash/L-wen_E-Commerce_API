using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
namespace Löwen.Application.Features.CouponFeature.Commands.AddCoupon;

internal class AddCouponCommandHandler(ICouponService couponService) : ICommandHandler<AddCouponCommand>
{
    public async Task<Result> Handle(AddCouponCommand command, CancellationToken ct)
    {

        var createResult = await couponService.AddAsync(new Coupon
        {
            DiscountType = command.DiscountType,
            DiscountValue = command.DiscountValue,
            Code = command.Code,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            IsActive = command.IsActive,
            UsageLimit = command.UsageLimit,

        }, ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
    }
}
