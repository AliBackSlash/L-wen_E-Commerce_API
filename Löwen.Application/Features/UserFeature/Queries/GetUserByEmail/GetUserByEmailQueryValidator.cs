namespace Löwen.Application.Features.UserFeature.Queries.GetUserByEmail;

public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.email).EmailAddress().NotEmpty().WithMessage("Valid Email Is Required");
    }
}
