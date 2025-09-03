namespace Löwen.Application.Features.SendEmailFeature.SendResetTokenCommand;

public record SendResetTokenCommand(string email) : ICommand;

