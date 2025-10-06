using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IProductImges
{
    Task<Result<Image?>> GetImageByPath(string path,CancellationToken ct);
    Task<Result> AddRangeAsync(IEnumerable<Image> images, CancellationToken ct);
    Task<Result> UpdateAsync(Image image, CancellationToken ct);

}