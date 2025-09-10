namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminByEmail;

public class GetAdminByEmailQueryValidator : AbstractValidator<GetAdminByEmailQuery>
{
    public GetAdminByEmailQueryValidator()
    {
        RuleFor(x => x.email).EmailAddress().WithMessage("enter a valid email");
    }
}
