
using Löwen.Application.Features.AuthFeature.Commands.RegisterCommand;
using Löwen.Application.Features.UserFeature.Commands.UpdateUserInfoCommand;
using Löwen.Domain.Abstractions.IServices.IAppUserServices;
using Löwen.Domain.Abstractions.IServices.IEmailServices;
using Löwen.Domain.Enums;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.AddAdmin;

public class AddAdminCommandHandler(IAppUserService userService,IEmailService emailService) : ICommandHandler<AddAdminCommand>
{
    public async Task<Result> Handle(AddAdminCommand command, CancellationToken ct)
    {
        var checkEmail = await userService.IsEmailNotTakenAsync(command.Email);
        if (checkEmail.IsFailure) return Result.Failure(checkEmail.Errors);

        var checkUserName = await userService.IsUserNameNotTakenAsync(command.UserName);
        if (checkUserName.IsFailure) return Result.Failure(checkUserName.Errors);

        var createResult = await userService.AddAdminAsync(new AddAdminDto(command.Email, command.UserName, command.Password,
             command.FName, command.MName, command.LName, command.DateOfBirth, command.PhoneNumber, command.Gender));
        if (createResult.IsFailure) return Result.Failure(createResult.Errors);

        var roleResult = await userService.AssignUserToRoleAsync(createResult.Value, UserRole.Admin);
        if (roleResult.IsFailure)
        {
            await userService.RemoveUserAsync(createResult.Value);
            return Result.Failure(roleResult.Errors);
        }

        var confirmationLink = await userService.GenerateEmailConfirmationTokenAsync(command.Email);
        if (confirmationLink.IsFailure) return Result.Failure(confirmationLink.Errors);

        var emailResult = await emailService.SendVerificationEmailAsync(command.Email, confirmationLink.Value, ct);
        if (emailResult.IsFailure)
            return Result.Failure(
                new Error("there are Confirm Email Errors", string.Join(", ", emailResult.Errors), ErrorType.ConfirmEmailError));

        return Result.Success();
    }
}
