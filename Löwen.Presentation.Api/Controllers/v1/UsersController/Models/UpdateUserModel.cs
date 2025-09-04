namespace Löwen.Presentation.Api.Controllers.v1.UsersController.Models;

public record UpdateUserModel(string token, string? FName, string? MName, string? LName, string? phoneNumber);
