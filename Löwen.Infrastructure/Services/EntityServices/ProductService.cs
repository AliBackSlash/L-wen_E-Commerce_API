using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ProductService(AppDbContext _context) : BasRepository<Product, Guid>(_context), IProductService
{
    public async Task<bool> IsFound(Guid Id,CancellationToken ct) => await _context.Products.AnyAsync(t => t.Id == Id,ct);

}
