using Löwen.Domain.Abstractions.IServices.IAppUserServices;
using Löwen.Domain.Abstractions.IServices.IEmailServices;
using Löwen.Domain.Enums;

namespace Löwen.Application.Features.AuthFeature.Commands.RegisterCommand;

public class RegisterCommandHandler(IEmailService emailService, IAppUserService userService) : ICommandHandler<RegisterCommand, RegisterCommandResponse>
{
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand command, CancellationToken ct)
    {
        var checkEmail = await userService.IsEmailNotTakenAsync(command.Email);
        if (checkEmail.IsFailure) return Result.Failure<RegisterCommandResponse>(checkEmail.Errors);

        var checkUserName = await userService.IsUserNameNotTakenAsync(command.UserName);
        if (checkUserName.IsFailure) return Result.Failure<RegisterCommandResponse>(checkUserName.Errors);

        var registerResult = await userService.RegisterAsync(new RegisterUserDto(command.Email, command.UserName, command.Password), ct);
        if (registerResult.IsFailure) return Result.Failure<RegisterCommandResponse>(registerResult.Errors);

        var roleResult = await userService.AssignUserToRoleAsync(registerResult.Value.Id, UserRole.User);
        if (roleResult.IsFailure)
        {
            await userService.RemoveUserAsync(registerResult.Value.Id.ToString());
            return Result.Failure<RegisterCommandResponse>(roleResult.Errors);
        }


        var confirmationLink = await userService.GenerateEmailConfirmationTokenAsync(command.Email);
        if (confirmationLink.IsFailure) return Result.Failure<RegisterCommandResponse>(confirmationLink.Errors);

        var emailResult = await emailService.SendVerificationEmailAsync(command.Email, confirmationLink.Value, ct);
        if (emailResult.IsFailure)
            return Result.Failure<RegisterCommandResponse>(
                new Error("there are Confirm Email Errors", string.Join(", ", emailResult.Errors), ErrorType.ConfirmEmailError));

       

        return Result.Success(new RegisterCommandResponse(registerResult.Value.Id, registerResult.Value.Token));
    }
}