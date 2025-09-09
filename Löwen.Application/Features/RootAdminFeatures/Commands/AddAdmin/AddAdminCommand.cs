namespace Löwen.Application.Features.RootAdminFeatures.Commands.AddAdmin;

public record AddAdminCommand(string Email, string UserName, string Password,
    string? FName, string? MName, string? LName, DateOnly DateOfBirth,string? PhoneNumber, char Gender) : ICommand;

