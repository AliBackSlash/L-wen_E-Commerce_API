namespace Löwen.Application.Features.AdminFeature.Commands.Tag.UpdateTag;

public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .Must(x => Guid.TryParse(x,out _)).WithMessage("Enter a valid Guid productId");
        RuleFor(x => x.Tag).NotEmpty().WithMessage("Tag is not null")
              .MaximumLength(100).WithMessage("the max Tag length is 100 chars");

    }
}
