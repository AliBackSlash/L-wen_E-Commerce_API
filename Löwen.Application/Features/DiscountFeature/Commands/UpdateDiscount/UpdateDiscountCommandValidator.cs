namespace Löwen.Application.Features.DiscountFeature.Commands.UpdateDiscount;

public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
{
  public UpdateDiscountCommandValidator()
  {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Id must be a valid GUID.");

        RuleFor(x => x.Name)
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
            .When(x => x.Name != null);

        RuleFor(x => x.DiscountType)
            .IsInEnum().WithMessage("DiscountType must be valid.")
            .When(x => x.DiscountType != null);

        RuleFor(x => x.DiscountValue)
            .GreaterThan(-1).WithMessage("DiscountValue must be positive number")
            .When(x => x.DiscountValue != null);

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .When(x => x.StartDate != null);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be after StartDate.")
            .When(x => x.EndDate != null && x.StartDate != null);

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("IsActive is required.")
            .When(x => x.IsActive != null);
    }
}
