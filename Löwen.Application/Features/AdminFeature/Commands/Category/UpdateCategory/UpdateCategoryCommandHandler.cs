using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;

namespace Löwen.Application.Features.AdminFeature.Commands.Category.UpdateCategory;

public class UpdateCategoryCommandHandler(IProductCategoryService categoryService) : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(UpdateCategoryCommand command, CancellationToken ct)
    {
        
        var category = await categoryService.GetByIdAsync(Guid.Parse(command.Id), ct);
       
        if (category == null)
            return Result.Failure(new Error("Category.Update", "Category not found", ErrorType.Conflict));

        category.Category = command.Category ?? category.Category;
        category.Gender = command.Gender ?? category.Gender;

     
        var updateResult = await categoryService.UpdateAsync(category, ct);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Errors);

        return Result.Success();
    }
}
