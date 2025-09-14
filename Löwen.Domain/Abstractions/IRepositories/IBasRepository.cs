
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Pagination;

namespace IslamicFace.Domain.Abstractions.IRepositories;

public interface IBasRepository<TEntity, IdType> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(IdType id, CancellationToken ct);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct);
    Task<Result> AddAsync(TEntity entity, CancellationToken ct);
    Task<Result>  UpdateAsync(TEntity entity, CancellationToken ct);
    Task<Result>  DeleteAsync(TEntity entity, CancellationToken ct);
    Task<PagedResult<TEntity>> GetPagedAsync(
    IQueryable<TEntity> query, PaginationParams paginationParams, CancellationToken ct = default);
}
