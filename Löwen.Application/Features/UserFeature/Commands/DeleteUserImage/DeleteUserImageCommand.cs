using Löwen.Application.Messaging.ICommand;
using Löwen.Domain.Layer_Dtos.Product;

namespace Löwen.Application.Features.UserFeature.Commands.DeleteUserImage;

public record DeleteUserImageCommand(string uId) : ICommand<string>;
