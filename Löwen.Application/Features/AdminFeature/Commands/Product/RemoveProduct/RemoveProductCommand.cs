using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.AdminFeature.Commands.Product.RemoveProduct;

public record RemoveProductCommand(string Id) : ICommand;
