using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.RootAdminFeatures.Commands.RemoveAdminCommand;
public record RemoveAdminCommand(string Id) : ICommand;

