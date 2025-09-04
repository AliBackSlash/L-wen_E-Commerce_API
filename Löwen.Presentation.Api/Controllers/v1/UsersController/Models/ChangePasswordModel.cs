namespace Löwen.Presentation.Api.Controllers.v1.UsersController.Models;

public record ChangePasswordModel(string token, string CurrentPassword, string Password, string ConfermPassword);
