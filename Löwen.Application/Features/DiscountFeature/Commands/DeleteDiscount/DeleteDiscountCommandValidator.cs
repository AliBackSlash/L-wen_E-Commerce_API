namespace Löwen.Application.Features.DiscountFeature.Commands.DeleteDiscount;

public class DeleteDiscountCommandValidator : AbstractValidator<RemoveDiscountCommand>
{
  public DeleteDiscountCommandValidator()
  {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Id must be a valid GUID.");

       
    }
}
