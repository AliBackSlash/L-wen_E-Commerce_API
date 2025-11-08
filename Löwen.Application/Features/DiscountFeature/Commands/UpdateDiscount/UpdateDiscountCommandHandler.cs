using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Application.Features.DiscountFeature.Commands.UpdateDiscount;

internal class UpdateDiscountCommandHandler(IDiscountService discountService) : ICommandHandler<UpdateDiscountCommand>
{
    public async Task<Result> Handle(UpdateDiscountCommand command, CancellationToken ct)
    {
        var discount = await discountService.GetByIdAsync(Guid.Parse(command.Id), ct);
        if (discount is null)
            return Result.Failure(Error.NotFound("Discount.NotFound", $"Discount with Id {command.Id} not found"));

        discount.Name = command.Name ?? discount.Name;
        discount.DiscountType = command.DiscountType ?? discount.DiscountType;
        discount.DiscountValue = command.DiscountValue ?? discount.DiscountValue;
        discount.StartDate = command.StartDate ?? discount.StartDate;
        discount.EndDate = command.EndDate ?? discount.EndDate;
        discount.IsActive = command.IsActive ?? discount.IsActive;

        var updateResult = await discountService.UpdateAsync(discount, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
