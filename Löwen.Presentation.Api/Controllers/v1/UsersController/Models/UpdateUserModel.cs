namespace Löwen.Presentation.Api.Controllers.v1.UsersController.Models;

public record UpdateUserModel(string? FName, string? MName, string? LName
    , DateOnly DateOfBirth, string PhoneNumber, char Gender = 'M');
