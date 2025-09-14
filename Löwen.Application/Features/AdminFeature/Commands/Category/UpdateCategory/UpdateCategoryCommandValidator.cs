namespace Löwen.Application.Features.AdminFeature.Commands.Category.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x,out _)).WithMessage("Enter a valid Guid id");
        RuleFor(x => x.Category).Must(x => x == null || x.Length <= 50).WithMessage("the max Tag length is 50 chars");
        RuleFor(x => x.Gender)
            .Must(x => x == null || x == 'M' || x == 'F').WithMessage("Gender input must be \'M\' or \'F\'");
        RuleFor(x => x.AgeFrom).Must(x => x == null || x > 0 && x < 150).WithMessage("Enter a valid age");
        RuleFor(x => x.AgeTo).Must(x => x == null || x > 0 && x < 150).WithMessage("Enter a valid age");

    }
}
