using Löwen.Application.Features.DiscountFeature.Queries.Response;
using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Application.Features.DiscountFeature.Queries.GetById;

internal class GetDiscountByIdQueryHandler(IDiscountService discountService) : ICommandHandler<GetDiscountByIdQuery, DiscountResponse>
{
    public async Task<Result<DiscountResponse>> Handle(GetDiscountByIdQuery command, CancellationToken ct)
    {
        var discount = await discountService.GetByIdAsync(Guid.Parse(command.Id), ct);
        if (discount is null)
            return Result.Failure<DiscountResponse>(Error.NotFound("Discount.NotFound", $"Discount with Id {command.Id} not found"));

        return Result.Success(new DiscountResponse
        {
            Id = discount.Id,
            Name = discount.Name,
            DiscountType = discount.DiscountType,
            DiscountValue = discount.DiscountValue,
            StartDate = discount.StartDate,
            EndDate = discount.EndDate,
            IsActive = discount.IsActive,

        });
    }
}
