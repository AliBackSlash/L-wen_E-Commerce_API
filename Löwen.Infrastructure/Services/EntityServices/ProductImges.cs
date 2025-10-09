using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class ProductImges(AppDbContext _context) : IProductImges
{
    private readonly DbSet<Image> _db = _context.Set<Image>();

    public async Task<Result> AddRangeAsync(IEnumerable<Image> images, CancellationToken ct)
    {
        try
        {
            await _db.AddRangeAsync(images,ct);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {

            return Result.Failure(new Error($"IProductImges.AddRangeAsync", ex.InnerException.Message, ErrorType.Conflict));
        }
    }

    public async Task<Result> DeleteAsync(string imageName, CancellationToken ct)
    {
        //make sure to make another image to be main or not accept the remove operation
        var image = await _db.Where(i => i.Path == imageName).FirstOrDefaultAsync();
        if ((image is null))
              return Result.Failure(new Error("IProductImges.DeleteAsync", $"no image with name {imageName} found", ErrorType.Conflict));

        var otherImageToBeMainImage = await _db.Where(x => x.ProductId == image.ProductId && x.Path != image.Path).FirstOrDefaultAsync(ct);
        if(otherImageToBeMainImage is null)
            return Result.Failure(new Error("IProductImges.DeleteAsync", $"this is only image for this product the product must have image(s) with main image", ErrorType.Conflict));


        try
        {
            otherImageToBeMainImage.IsMain = true;
            _db.Update(otherImageToBeMainImage);
             _db.Remove(image);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("IProductImges.DeleteAsync", ex.InnerException.Message, ErrorType.InternalServer));
        }
    }
}
