namespace Löwen.Presentation.Api.Controllers.v1.RootAdminController.Models;

public record AddAdminModel(string Email, string UserName, string Password,
    string? FName, string? MName, string? LName, DateOnly DateOfBirth, string? PhoneNumber, char Gender);

