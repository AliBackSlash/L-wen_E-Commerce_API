namespace Löwen.Application.Features.AdminFeature.Commands.Category.UpdateCategory;

public record UpdateCategoryCommand(string Id, string? Category, char? Gender) : ICommand;
