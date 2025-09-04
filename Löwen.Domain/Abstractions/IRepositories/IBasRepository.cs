
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Pagination;

namespace IslamicFace.Domain.Abstractions.IRepositories;

public interface IBasRepository<TEntity, IdType> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(IdType id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<Result<int>> SaveChangesAsync(CancellationToken cancellationToken);
    Task<PagedResult<TEntity>> GetPagedAsync(
    IQueryable<TEntity> query, PaginationParams paginationParams, CancellationToken cancellationToken = default);
}
