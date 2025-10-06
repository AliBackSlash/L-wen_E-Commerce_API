using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProductImages;

public class AddProductImagesCommandHandler(IProductImges imges)
    : ICommandHandler<AddProductImagesCommand>
{
    public async Task<Result> Handle(AddProductImagesCommand command, CancellationToken ct)
    {
        var createResult = await imges.AddRangeAsync(command.images.Select(i => new Image
        {
            ProductId = i.ProductId,
            Path = i.Path,
            IsMain = i.IsMain,
        }), ct);

        if (createResult.IsFailure)
            return Result.Failure(createResult.Errors);

        return Result.Success();
    }
}
