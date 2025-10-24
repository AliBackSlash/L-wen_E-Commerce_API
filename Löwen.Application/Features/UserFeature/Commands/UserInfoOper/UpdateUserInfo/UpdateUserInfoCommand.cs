namespace Löwen.Application.Features.UserFeature.Commands.UserInfoOper.UpdateUserInfo;

public record UpdateUserInfoCommand(string Id,string? fName,string? mName,string? lName
    ,DateOnly? DateOfBirth, string? PhoneNumber, char? Gender, string AddressDetails) : ICommand<UpdateUserInfoCommandResponse>;