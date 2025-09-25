
using Löwen.Domain.ErrorHandleClasses;

namespace IslamicFace.Domain.Abstractions.IRepositories;

public interface IGetDeleteBasRepository<TEntity, IdType> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(IdType id, CancellationToken ct);
    Task<Result>  DeleteAsync(TEntity entity, CancellationToken ct);
}
