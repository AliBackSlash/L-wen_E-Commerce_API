using IslamicFace.Domain.Abstractions.IRepositories;
using Löwen.Domain.ErrorHandleClasses;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.EFCore.Repositories;

public class BasRepository<TEntity, IdType>(AppDbContext _context) : IBasRepository<TEntity, IdType> where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(IdType id, CancellationToken ct) => await _dbSet.FindAsync(id, ct);
    public async Task<Result<TEntity>> AddAsync(TEntity entity, CancellationToken ct)
    {
        
        try
        {
            await _dbSet.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            return Result.Failure<TEntity>(new Error($"{typeof(TEntity)}.Add", ex.Message, ErrorType.InternalServer));
        }
        
    }
    public async Task<Result> UpdateAsync(TEntity entity, CancellationToken ct)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"{typeof(TEntity)}.Update", ex.Message, ErrorType.InternalServer));
        }
    }
    public async Task<Result> DeleteAsync(TEntity entity, CancellationToken ct)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"{typeof(TEntity)}.Delete", ex.Message, ErrorType.InternalServer));
        }
    }

}
