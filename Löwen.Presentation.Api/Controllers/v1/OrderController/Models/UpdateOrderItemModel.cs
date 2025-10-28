namespace Löwen.Presentation.Api.Controllers.v1.OrderController.Models;

public record UpdateOrderItemModel(string OrderId,string deliveryId ,string ProductId, byte? Quantity, decimal? Price);
