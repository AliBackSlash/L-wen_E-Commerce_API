namespace Löwen.Application.Features.AdminFeature.Commands.Tag.UpdateTag;

public record UpdateTagCommand(string Id, string Tag) : ICommand;
