using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class OrderService(AppDbContext _context) : BasRepository<Order, Guid>(_context), IOrderService
{
    public async Task<bool> IsFound(Guid Id, CancellationToken ct) => await _context.Orders.AnyAsync(t => t.Id == Id, ct);
}

