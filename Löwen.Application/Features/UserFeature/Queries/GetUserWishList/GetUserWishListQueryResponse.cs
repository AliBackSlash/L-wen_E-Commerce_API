namespace Löwen.Application.Features.UserFeature.Queries.GetUserWishList;

public class GetUserWishListQueryResponse
{
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string Image { get; set; }
    public double Price { get; set; }
}