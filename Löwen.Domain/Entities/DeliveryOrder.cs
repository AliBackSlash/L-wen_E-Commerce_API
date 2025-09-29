namespace Löwen.Domain.Entities;

// ProductCategories Table
public class DeliveryOrder
{
    public Guid DeliveryId { get; set; }// => Delivery is a normal user with role Delivery
    public Guid OrderId { get; set; }

    // Navigation property
    public Order? Order { get; set; }
}
