using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ProductTagService(AppDbContext _context) : BasRepository<ProductTag, Guid>(_context), IProductTagService
{
}
