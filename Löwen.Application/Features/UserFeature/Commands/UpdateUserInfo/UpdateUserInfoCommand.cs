namespace Löwen.Application.Features.UserFeature.Commands.UpdateUserInfoCommand;

public record UpdateUserInfoCommand(string Id,string? fName,string? mName,string? lName
    ,DateOnly? DateOfBirth, string? PhoneNumber, char? Gender) : ICommand<UpdateUserInfoCommandResponse>;