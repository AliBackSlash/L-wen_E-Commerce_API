using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;

public class AddProductCommandHandler(IProductCategoryService categoryService,IProductService productService) : ICommandHandler<AddProductCommand>
{
    public async Task<Result> Handle(AddProductCommand command, CancellationToken ct)
    {
        Guid ProductId = Guid.Parse(command.CategoryId);

        if (await categoryService.IsFound(ProductId, ct))
            return Result.Failure(new Error("Product.Add", $"Category with Id {command.CategoryId} not found", ErrorType.Conflict));

        var addResult = await productService.AddAsync(new Domain.Entities.Product
        {
           Name = command.Name,
           Price = command.Price,
           StockQuantity = command.StockQuantity,
           CategoryId = ProductId,
           Description = command.Description,
           Status = command.Status,

        }, ct);

        if (addResult.IsFailure)
            return Result.Failure(addResult.Errors);

        return Result.Success();
    }
}
