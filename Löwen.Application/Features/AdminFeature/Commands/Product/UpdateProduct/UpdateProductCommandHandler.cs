using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler(IProductService productService,IProductCategoryService categoryService) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand command, CancellationToken ct)
    {

        var product = await productService.GetByIdAsync(Guid.Parse(command.Id), ct);
       
        if (product == null)
            return Result.Failure(new Error("Product.Update", "product not found", ErrorType.Conflict));
       
        if (command.CategoryId is not null)
            if (!await categoryService.IsFound(Guid.Parse(command.CategoryId), ct))
                return Result.Failure<Guid>(new Error("Product.Add", $"Category with Id {command.CategoryId} not found", ErrorType.Conflict));
            else
                product.CategoryId = Guid.Parse(command.CategoryId);

        product.Name = command.Name ?? product.Name;
        product.Description = command.Description ?? product.Description;
        product.MainPrice = command.MainPrice ?? product.MainPrice;  
        /* product.StockQuantity = command.StockQuantity ?? product.StockQuantity;  */
        product.Status = command.Status ?? product.Status;


        var updateResult = await productService.UpdateAsync(product, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
