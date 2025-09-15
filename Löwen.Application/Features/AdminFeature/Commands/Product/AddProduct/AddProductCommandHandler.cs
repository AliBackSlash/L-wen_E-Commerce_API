using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;

public class AddProductCommandHandler(IProductCategoryService categoryService,IProductService productService) : ICommandHandler<AddProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddProductCommand command, CancellationToken ct)
    {
        Guid CategoryId = Guid.Parse(command.CategoryId);
        if (!await categoryService.IsFound(CategoryId, ct))
            return Result.Failure<Guid>(new Error("Product.Add", $"Category with Id {CategoryId} not found", ErrorType.Conflict));

        

        var addResult = await productService.AddAsync(new Domain.Entities.Product
        {
           Name = command.Name,
           Price = command.Price,
           StockQuantity = command.StockQuantity,
           CategoryId = CategoryId,
           Description = command.Description,
           Status = command.Status,

        }, ct);

        if (addResult.IsFailure)
            return Result.Failure<Guid>(addResult.Errors);

        return Result.Success(addResult.Value.Id);
    }
}
