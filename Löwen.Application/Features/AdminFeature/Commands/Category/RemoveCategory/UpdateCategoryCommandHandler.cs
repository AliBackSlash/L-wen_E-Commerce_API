using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;

namespace Löwen.Application.Features.AdminFeature.Commands.Category.RemoveCategory;

public class RemoveCategoryCommandHandler(IProductCategoryService categoryService) : ICommandHandler<RemoveCategoryCommand>
{
    public async Task<Result> Handle(RemoveCategoryCommand command, CancellationToken ct)
    {
        
        var category = await categoryService.GetByIdAsync(command.Id, ct);
       
        if (category == null)
            return Result.Failure(new Error("Category.Delete", "Category not found", ErrorType.Conflict));

        var updateResult = await categoryService.DeleteAsync(category, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
