namespace Löwen.Application.Features.UserFeature.Queries.GetUserById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
             .Must(x => Guid.TryParse(x, out _)).WithMessage("Enter a valid Guid id");
    }
}
