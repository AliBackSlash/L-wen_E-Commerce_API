using static Löwen.Domain.ErrorHandleClasses.ErrorCodes;

namespace Löwen.Application.Features.AdminFeature.Queries.GetProducts;

public class GetProductsQueryResponse
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public double LoveCount { get; set; }
    public required string CreatedBy { get; set; }
    public double MainPrice { get; set; }
    public ProductStatus Status { get; set; }

}
