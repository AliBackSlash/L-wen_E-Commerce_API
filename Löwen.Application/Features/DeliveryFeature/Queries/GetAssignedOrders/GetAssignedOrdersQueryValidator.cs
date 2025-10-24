namespace Löwen.Application.Features.DeliveryFeature.Queries.GetAssignedOrders;

public class GetAssignedOrdersQueryValidator : AbstractValidator<GetAssignedOrdersQuery>
{
  public GetAssignedOrdersQueryValidator()
  {
        RuleFor(x => x.DeliveryId)
            .NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid user id");
       
    }
}
