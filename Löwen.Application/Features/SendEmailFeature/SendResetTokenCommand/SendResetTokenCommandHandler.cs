using Löwen.Application.Abstractions.IServices.IdentityServices;
using Löwen.Application.Features.AuthFeature.Commands.ResetPasswordCommand;
using Löwen.Domain.Abstractions.IServices;

namespace Löwen.Application.Features.SendEmailFeature.SendResetTokenCommand;

internal class SendResetTokenCommandHandler(IAppUserService userService,IEmailService emailService) : ICommandHandler<SendResetTokenCommand>
{
    public async Task<Result> Handle(SendResetTokenCommand command, CancellationToken cancellationToken)
    {
        var GenerateResult = await userService.GenerateRestPasswordTokenAsync(command.email);
        if (GenerateResult.IsFailure)
            return Result.Failure<ResetPasswordCommandResponse>(GenerateResult.Errors);

        var SendResult = await emailService.SendRestPasswordTokenAsync(command.email, GenerateResult.Value, cancellationToken);
        if (SendResult.IsFailure)
            return SendResult;

        return Result.Success();
    }
}
