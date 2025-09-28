using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class OrderItemService(AppDbContext _context) : BasRepository<OrderItem, Guid>(_context), IOrderItemsService
{
    public async Task<OrderItem> GetOrderItem(Guid orderId, Guid productId,CancellationToken ct) => await _context.OrderItems.
        Where(oi => oi.OrderId == orderId && oi.ProductId == productId).FirstOrDefaultAsync(ct);
}