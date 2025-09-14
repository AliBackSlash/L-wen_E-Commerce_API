using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ProductCategoryService(AppDbContext _context) : BasRepository<ProductCategory, Guid>(_context), IProductCategoryService
{
    public async Task<bool> IsFound(Guid Id,CancellationToken ct) => await _context.ProductCategories.AnyAsync(c => c.Id == Id,ct);


}
