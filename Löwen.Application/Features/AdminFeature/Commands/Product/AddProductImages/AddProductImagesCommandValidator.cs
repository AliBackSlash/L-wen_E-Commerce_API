
using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProductImages;

public class AddProductImagesCommandValidator : AbstractValidator<AddProductImagesCommand>
{
    public AddProductImagesCommandValidator()
    {
        RuleFor(x => x.images).Must(x => x.Count() > 0).WithMessage("At least one image must be provided.");
    }

    //bool IsThereOneMain(List<AddProductImagesDto> images) => images.ForEach(x => x.IsMain) == 1;
}
