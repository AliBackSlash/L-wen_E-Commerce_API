namespace Löwen.Domain.Layer_Dtos.WishList;

public class UserWishlistDto
{
    public Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string Image { get; set; }
    public double Price {  get; set; }

}
