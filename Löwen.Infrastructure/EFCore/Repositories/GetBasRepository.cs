using IslamicFace.Domain.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.EFCore.Repositories;

public class GetBasRepository<TEntity, IdType>(AppDbContext Context) : IGetBasRepository<TEntity, IdType> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = Context.Set<TEntity>();
    public async Task<TEntity?> GetByIdAsync(IdType id, CancellationToken ct) => await _dbSet.FindAsync(id, ct);

}
