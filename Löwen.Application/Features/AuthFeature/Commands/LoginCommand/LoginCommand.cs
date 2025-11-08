using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AuthFeature.Commands.LoginCommand;

public record LoginCommand(string UserNameOrEmail, string? Password) : ICommand<LoginCommandResponse>;
