namespace Löwen.Application.Features.OrderFeature.Commands.UpdateOrderItem.UpdateOrderItem;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(x => x.orderId).NotEmpty().WithMessage("order id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid order id");
     
        RuleFor(x => x.deliveryId)
          .Must(x => x is null || Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid delivery id");

        RuleFor(x => x.productId).NotEmpty().WithMessage("product Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid product id");

        RuleFor(x => x.Quantity).Must(x => x is  null || x >= 0 && x <= 255)
            .WithMessage("enter quantity between 0 and 255");

        RuleFor(x => x.PriceAtPurchase).Must(x => x is  null || x > 0)
            .WithMessage("enter quantity between 0 and 255");

        
    }

}
