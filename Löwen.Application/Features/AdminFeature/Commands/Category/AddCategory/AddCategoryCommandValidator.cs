namespace Löwen.Application.Features.AdminFeature.Commands.Category.AddCategory;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is not null")
              .MaximumLength(50).WithMessage("the max Tag length is 50 chars");
        RuleFor(x => x.Gender).NotEmpty().WithMessage("Category is not null")
            .Must(x => x == 'M' || x == 'F').WithMessage("Gender input must be \'M\' or \'F\'");


    }
}
