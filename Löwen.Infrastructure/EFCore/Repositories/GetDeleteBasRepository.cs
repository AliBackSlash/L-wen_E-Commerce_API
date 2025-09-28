using IslamicFace.Domain.Abstractions.IRepositories;
using Löwen.Domain.ErrorHandleClasses;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.EFCore.Repositories;

public class GetDeleteBasRepository<TEntity, IdType>(AppDbContext _context) : IGetDeleteBasRepository<TEntity, IdType> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(IdType id, CancellationToken ct) => await _dbSet.FindAsync(id, ct);

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
            return Result.Failure(new Error($"{nameof(TEntity)}.Delete", ex.Message, ErrorType.InternalServer));
        }
    }


}