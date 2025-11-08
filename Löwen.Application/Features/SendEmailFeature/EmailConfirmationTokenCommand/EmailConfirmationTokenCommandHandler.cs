
using Löwen.Application.Features.AuthFeature.Commands.RegisterCommand;
using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Abstractions.IServices.IEmailServices;

namespace Löwen.Application.Features.SendEmailFeature.EmailConfirmationTokenCommand;

internal class EmailConfirmationTokenCommandHandler(IAppUserService userService,IEmailService emailService) : ICommandHandler<EmailConfirmationTokenCommand>
{
    public async Task<Result> Handle(EmailConfirmationTokenCommand command, CancellationToken ct)
    {
        var confirmationLink = await userService.GenerateEmailConfirmationTokenAsync(command.email);
        if (confirmationLink.IsFailure) return Result.Failure(confirmationLink.Errors);

        var emailResult = await emailService.SendVerificationEmailAsync(command.email, confirmationLink.Value, ct);
        if (emailResult.IsFailure)
            return Result.Failure(
                new Error("there are Confirm Email Errors", string.Join(", ", emailResult.Errors), ErrorType.ConfirmEmailError));
        return Result.Success();
    }
}
