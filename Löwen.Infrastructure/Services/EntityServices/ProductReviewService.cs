using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ProductReviewService(AppDbContext _context) : BasRepository<ProductReview, Guid>(_context), IProductReviewService
{
    public async Task<bool> IsFound(Guid Id,CancellationToken ct) => await _context.Products.AnyAsync(t => t.Id == Id,ct);

}
