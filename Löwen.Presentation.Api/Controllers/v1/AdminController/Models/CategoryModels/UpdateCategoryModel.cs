namespace Löwen.Presentation.Api.Controllers.v1.AdminController.Models.CategoryModels;

public record UpdateCategoryModel(string Id, string? Category, char? Gender, byte? AgeFrom, byte? AgeTo);