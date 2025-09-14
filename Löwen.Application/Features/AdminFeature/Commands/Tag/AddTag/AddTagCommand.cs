namespace Löwen.Application.Features.AdminFeature.Commands.Tag.AddTag;

public record AddTagCommand(string Tag,string productId) : ICommand;
