using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
using static Löwen.Domain.ErrorHandleClasses.ErrorCodes;

namespace Löwen.Application.Features.UserFeature.Commands.Love.AddOrder;

internal class AddOrderCommandHandler(IOrderService orderService) : ICommandHandler<AddOrderCommand, string>
{
    public async Task<Result<string>> Handle(AddOrderCommand request, CancellationToken ct)
    {
        var createResult = await orderService.AddAsync(new Order
        {
            UserId = Guid.Parse(request.UserId),
            Status = OrderStatus.Pending,
        }, ct);

        if (createResult.IsFailure)
            return Result.Failure<string>(createResult.Errors);

        return Result.Success(createResult.Value.Id.ToString());
    }
}
