namespace Löwen.Presentation.Api.Controllers.v1.UsersController.Models;

public record ChangePasswordModel(string CurrentPassword, string Password, string ConfermPassword);
