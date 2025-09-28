namespace IslamicFace.Domain.Abstractions.IRepositories;

public interface IGetBasRepository<TEntity, IdType> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(IdType id, CancellationToken ct);
}
