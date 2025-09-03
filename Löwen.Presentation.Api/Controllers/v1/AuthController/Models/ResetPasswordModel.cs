
namespace Löwen.Presentation.API.Controllers.v1.AuthController.Models;

public record ResetPasswordModel(string Email, string token, string Password, string ConfermPassword);
