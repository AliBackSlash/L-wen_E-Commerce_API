
using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AuthFeature.Commands.ConfirmEmailCommand;

public record ConfirmEmailCommand(string userId, string Token) : ICommand<ConfirmEmailResponse>;

