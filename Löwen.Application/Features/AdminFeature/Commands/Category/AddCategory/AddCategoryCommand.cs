namespace Löwen.Application.Features.AdminFeature.Commands.Category.AddCategory;

public record AddCategoryCommand(string? Category, char Gender) : ICommand;
