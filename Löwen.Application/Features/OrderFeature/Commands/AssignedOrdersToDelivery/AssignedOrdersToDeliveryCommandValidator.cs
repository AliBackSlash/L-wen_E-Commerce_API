namespace Löwen.Application.Features.OrderFeature.Commands.AssignedOrdersToDelivery;

public class AssignedOrdersToDeliveryCommandValidator : AbstractValidator<AssignedOrdersToDeliveryCommand>
{
  public AssignedOrdersToDeliveryCommandValidator()
  {

        RuleFor(x => x.orders)
       .Must(items => items != null && items.Any()).WithMessage("At least one item is required in the oder");

    }
}
