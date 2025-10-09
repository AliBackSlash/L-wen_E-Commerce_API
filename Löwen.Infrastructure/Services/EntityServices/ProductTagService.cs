using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ProductTagService(AppDbContext _context) : BasRepository<ProductTag, Guid>(_context), IProductTagService
{
    public async Task<ProductTag?> GetByProductIdAsync(Guid productId, CancellationToken ct)
        => await _dbSet.Where(x => x.ProductId == productId).FirstOrDefaultAsync(ct);
}
