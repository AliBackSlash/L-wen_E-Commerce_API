using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.DiscountFeature.Commands.AddDiscount;

internal class AddDiscountCommandHandler(IDiscountService discountService) : ICommandHandler<AddDiscountCommand>
{
    public async Task<Result> Handle(AddDiscountCommand command, CancellationToken ct)
    {
        var discount = new Discount
        {
            Name = command.Name,
            DiscountType = command.DiscountType,
            DiscountValue = command.DiscountValue,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            IsActive = command.IsActive,
        };

        var result = await discountService.AddAsync(discount, ct);

        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Success();
    }
}
