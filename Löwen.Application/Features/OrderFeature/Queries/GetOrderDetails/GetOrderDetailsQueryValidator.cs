namespace Löwen.Application.Features.UserFeature.Queries.GetOrderDetails;

public class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
{
    public GetOrderDetailsQueryValidator()
    {
        RuleFor(x => x.orderId).NotEmpty().WithMessage("Id is required")
          .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid order id");    
    }

}
