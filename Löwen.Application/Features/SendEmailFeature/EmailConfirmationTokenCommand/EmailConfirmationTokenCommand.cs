using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.SendEmailFeature.EmailConfirmationTokenCommand;

public record EmailConfirmationTokenCommand(string email) : ICommand;

