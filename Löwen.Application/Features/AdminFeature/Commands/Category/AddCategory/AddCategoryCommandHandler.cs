using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;

namespace Löwen.Application.Features.AdminFeature.Commands.Category.AddCategory;

public class AddCategoryCommandHandler(IProductCategoryService categoryService) : ICommandHandler<AddCategoryCommand>
{
    public async Task<Result> Handle(AddCategoryCommand command, CancellationToken ct)
    {
        var addResult = await categoryService.AddAsync(new ProductCategory
        {
            Category = command.Category,
            Gender = command.Gender,
            AgeFrom = command.AgeFrom,
            AgeTo = command.AgeTo
        },ct);

        if (addResult.IsFailure)
            return Result.Failure(addResult.Errors);

        return Result.Success();
    }
}
