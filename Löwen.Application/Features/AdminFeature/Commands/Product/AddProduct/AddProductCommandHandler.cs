using Löwen.Domain.Abstractions.IServices.IEntitiesServices;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.AddProduct;

public class AddProductCommandHandler(IProductCategoryService categoryService,IProductService productService) : ICommandHandler<AddProductCommand, AddProductCommandResponse>
{
    public async Task<Result<AddProductCommandResponse>> Handle(AddProductCommand command, CancellationToken ct)
    {
        Guid CategoryId = Guid.Parse(command.CategoryId);
        if (!await categoryService.IsFound(CategoryId, ct))
            return Result.Failure<AddProductCommandResponse>(new Error("Product.Add", $"Category with Id {CategoryId} not found", ErrorType.Conflict));



        var addResult = await productService.AddAsync(new Domain.Entities.Product
        {
            Name = command.Name,
            CategoryId = CategoryId,
            Description = command.Description,
            Status = command.Status,
            CreatedBy = Guid.Parse(command.CreatedBy),
            Tags = command.Tags,
            ProductVariants = command.VariantDtos.Select(v => new ProductVariant
            {
                ColorId = v.ColorId,
                SizeId = v.SizeId,
                Price = v.Price,
                StockQuantity = v.StockQuantity,
            }).ToList(),


        }, ct);

        if (addResult.IsFailure)
            return Result.Failure<AddProductCommandResponse>(addResult.Errors);

        return Result.Success(new AddProductCommandResponse
        {
            ProductId = addResult.Value.Id
        });
    }
}
