using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AuthFeature.Commands.RefreshToken;

public record RefreshTokenCommand(string refreshToken) : ICommand<RefreshTokenCommandResponse>;
