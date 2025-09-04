namespace Löwen.Application.Features.UserFeature.Queries;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.token)
         .NotEmpty()
         .WithMessage("token is required");
    }
}
