using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Layer_Dtos.Delivery;
using Löwen.Domain.Pagination;

namespace Löwen.Infrastructure.Services.EntityServices;

public class DeliveryService(AppDbContext _context) : IDeliveryService
{
    /*
        public required string ProductImageUrl { get; set; }
        public required string ProductName { get; set; }
        public required string AddressDetails { get; set; }
        public required string CustomerPhoneNum { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
    */
    public Task<PagedResult<GetAssignedOrdersDto>> GetAssignedOrdersAsync(Guid userId, PaginationParams parm, CancellationToken ct)
    {
        var query = from del_order in _context.DeliveryOrders
                    join order in _context.Orders on del_order.OrderId equals order.Id
                    join customer in _context.Users on order.

        throw new NotImplementedException();
    }
}