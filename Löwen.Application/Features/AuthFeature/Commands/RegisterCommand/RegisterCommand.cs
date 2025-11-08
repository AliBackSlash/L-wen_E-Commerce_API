using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AuthFeature.Commands.RegisterCommand;
public record RegisterCommand(string Email, string UserName, string Password) : ICommand;
