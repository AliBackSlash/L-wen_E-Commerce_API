using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.DeleteProductImages;

public class DeleteProductImagesCommandHandler(IProductImges imges)
    : ICommandHandler<DeleteProductImagesCommand>
{
    public async Task<Result> Handle(DeleteProductImagesCommand command, CancellationToken ct)
    {
        var deleteResult = await imges.DeleteAsync(command.imageName, ct);

        if (deleteResult.IsFailure)
            return Result.Failure(deleteResult.Errors);

        return Result.Success();
    }
}
