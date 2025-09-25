
using Löwen.Domain.ErrorHandleClasses;

namespace IslamicFace.Domain.Abstractions.IRepositories;

public interface ICollectionBasRepository<TEntity, IdType> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(IdType id, CancellationToken ct);
    Task<Result> AddAsync(IEnumerable<TEntity> entity, CancellationToken ct);
    Task<Result>  UpdateAsync(IEnumerable<TEntity> entity, CancellationToken ct);
    Task<Result>  DeleteAsync(IEnumerable<TEntity> entity, CancellationToken ct);
}
