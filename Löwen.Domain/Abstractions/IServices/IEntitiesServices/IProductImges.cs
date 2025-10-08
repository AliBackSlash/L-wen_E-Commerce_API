using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductImges
{
    Task<Result> AddRangeAsync(IEnumerable<Image> images, CancellationToken ct);
    Task<Result> DeleteAsync(string imageName, CancellationToken ct);

}