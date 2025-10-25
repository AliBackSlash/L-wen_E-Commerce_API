using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Discount;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class DiscountService(AppDbContext _context) : BasRepository<Discount, Guid>(_context), IDiscountService
{
    public async Task<bool> IsHaveSameDisName(string DisName, CancellationToken ct) => await _dbSet.AnyAsync(x => x.Name == DisName);
    

    public async Task<PagedResult<DiscountDto>> GetAllPaged(PaginationParams prm, CancellationToken ct)
    {
        var query = from dis in _dbSet select dis;

        
        var totalCount = await query.CountAsync();
        var items = await query.Select(d => new DiscountDto
        {
            Id = d.Id,
            Name = d.Name,
            DiscountType = d.DiscountType,
            DiscountValue = d.DiscountValue,
            StartDate = d.StartDate,
            EndDate = d.EndDate,
            IsActive = d.IsActive
        }).Skip(prm.Skip).Take(prm.Take).ToListAsync();
        return PagedResult<DiscountDto>.Create(items, totalCount, prm.PageNumber, prm.Take);
    }

    public async Task<bool> IsFound(Guid Id, CancellationToken ct) => await _dbSet.AnyAsync(t => t.Id == Id, ct);

    
}
