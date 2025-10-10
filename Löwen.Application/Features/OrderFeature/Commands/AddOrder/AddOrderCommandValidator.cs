namespace Löwen.Application.Features.OrderFeature.Commands.AddOrder.AddOrder;

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
  public AddOrderCommandValidator()
  {

        RuleFor(x => x.deliveryId).NotEmpty().WithMessage("delivry Id is required")
      .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid delivary Id");

        RuleFor(x => x.items)
       .Must(items => items != null && items.Any()).WithMessage("At least one item is required in the oder");

    }
}
