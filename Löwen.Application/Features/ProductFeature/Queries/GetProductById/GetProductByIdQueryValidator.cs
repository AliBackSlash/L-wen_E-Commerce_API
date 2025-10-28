namespace Löwen.Application.Features.ProductFeature.Queries.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.productId).NotEmpty().WithMessage("productId is required")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid productId");
    }
}
