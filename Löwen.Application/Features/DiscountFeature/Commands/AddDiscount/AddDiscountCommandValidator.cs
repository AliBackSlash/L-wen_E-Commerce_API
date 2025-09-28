namespace Löwen.Application.Features.DiscountFeature.Commands.AddDiscount;

public class AddDiscountCommandValidator : AbstractValidator<AddDiscountCommand>
{
  public AddDiscountCommandValidator()
  {
    RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required.")
        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

    RuleFor(x => x.DiscountType)
        .IsInEnum().WithMessage("DiscountType is required and must be valid.");

    RuleFor(x => x.DiscountValue)
        .NotNull().WithMessage("DiscountValue is required.")
        .GreaterThan(-1).WithMessage("DiscountValue must be positive number");

    RuleFor(x => x.StartDate)
        .NotEmpty().WithMessage("StartDate is required.");

    RuleFor(x => x.EndDate)
        .NotEmpty().WithMessage("EndDate is required.")
        .GreaterThan(x => x.StartDate).WithMessage("EndDate must be after StartDate.");

    RuleFor(x => x.IsActive)
        .NotNull().WithMessage("IsActive is required.");
  }
}
