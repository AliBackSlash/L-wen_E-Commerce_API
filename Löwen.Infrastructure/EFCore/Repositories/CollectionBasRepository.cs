using IslamicFace.Domain.Abstractions.IRepositories;
using Löwen.Domain.ErrorHandleClasses;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.EFCore.Repositories;

public class CollectionBasRepository<TEntity, IdType>(AppDbContext _context) : ICollectionBasRepository<TEntity, IdType> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(IdType id, CancellationToken ct) => await _dbSet.FindAsync(id, ct);
    public async Task<Result> AddAsync(IEnumerable<TEntity> entity, CancellationToken ct)
    {

        try
        {
            await _dbSet.AddRangeAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure<TEntity>(new Error($"{nameof(TEntity)}.AddRange", ex.Message, ErrorType.InternalServer));
        }

    }
    public async Task<Result> UpdateAsync(IEnumerable<TEntity> entity, CancellationToken ct)
    {
        try
        {
            _dbSet.UpdateRange(entity);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"{nameof(TEntity)}.UpdateRange", ex.Message, ErrorType.InternalServer));
        }
    }
    public async Task<Result> DeleteAsync(IEnumerable<TEntity> entity, CancellationToken ct)
    {
        try
        {
            _dbSet.RemoveRange(entity);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"{nameof(TEntity)}.RemoveRange", ex.Message, ErrorType.InternalServer));
        }
    }
}