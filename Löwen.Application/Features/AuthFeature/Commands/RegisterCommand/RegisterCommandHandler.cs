using Löwen.Domain.Abstractions.IServices;
using Löwen.Application.Abstractions.IServices.IdentityServices;
using Löwen.Domain.Enums;

namespace Löwen.Application.Features.AuthFeature.Commands.RegisterCommand;

public class RegisterCommandHandler(IEmailService emailService, IAppUserService userService) : ICommandHandler<RegisterCommand, RegisterCommandResponse>
{
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var checkEmail = await userService.IsEmailNotTakenAsync(command.Email);
        if (checkEmail.IsFailure) return Result.Failure<RegisterCommandResponse>(checkEmail.Errors);

        var checkUserName = await userService.IsUserNameNotTakenAsync(command.UserName);
        if (checkUserName.IsFailure) return Result.Failure<RegisterCommandResponse>(checkUserName.Errors);

        var registerResult = await userService.RegisterAsync(new RegisterUserDto(command.Email, command.UserName, command.Password), cancellationToken);
        if (registerResult.IsFailure) return Result.Failure<RegisterCommandResponse>(registerResult.Errors);

        var roleResult = await userService.AddUserToRoleAsync(registerResult.Value.Id, UserRole.User);
        if (roleResult.IsFailure)
        {
            await userService.DeleteUserAsync(registerResult.Value.Id);
            return Result.Failure<RegisterCommandResponse>(roleResult.Errors);
        }


        var confirmationLink = await userService.GenerateEmailConfirmationTokenAsync(command.Email);
        if (confirmationLink.IsFailure) return Result.Failure<RegisterCommandResponse>(confirmationLink.Errors);

        var emailResult = await emailService.SendVerificationEmailAsync(command.Email, confirmationLink.Value, cancellationToken);
        if (emailResult.IsFailure)
            return Result.Failure<RegisterCommandResponse>(
                new Error("there are Confirm Email Errors", string.Join(", ", emailResult.Errors), ErrorType.ConfirmEmailError));

       

        return Result.Success(new RegisterCommandResponse(registerResult.Value.Id, registerResult.Value.Token));
    }
}