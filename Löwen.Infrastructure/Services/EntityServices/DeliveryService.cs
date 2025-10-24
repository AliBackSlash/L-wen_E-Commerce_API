using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Layer_Dtos.Delivery;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class DeliveryService(AppDbContext _context) : IDeliveryService
{
    public async Task<PagedResult<GetAssignedOrdersDto>> GetAssignedOrdersAsync(Guid deliveryId, PaginationParams parm, CancellationToken ct)
    {
        var query = from del_order in _context.DeliveryOrders
                    join order in _context.Orders on del_order.OrderId equals order.Id
                    join customer in _context.Users on order.CustomerId equals customer.Id
                    where del_order.DeliveryId == deliveryId
                    orderby order.OrderDate ascending
                    select new
                    {
                        CustomerName = customer.FName + ' ' + customer.LName,
                        customer.AddressDetails,
                        CustomerPhoneNum = customer.PhoneNumber,
                        Total = (_context.OrderItems.Where(oi => oi.OrderId == order.Id).Sum(x => x.Price * x.Quantity)),
                        OrderStatus = order.Status,
                    };
        var totalCount = await query.CountAsync();
        var items = await query.Select(i => new GetAssignedOrdersDto
        {
            CustomerName = i.CustomerName,
            AddressDetails =i.AddressDetails,
            CustomerPhoneNum = i.CustomerPhoneNum,
            Total = i.Total,
            Status = i.OrderStatus
        }).Skip(parm.Skip).Take(parm.Take)
        .AsNoTracking()
        .ToListAsync();

        return PagedResult<GetAssignedOrdersDto>.Create(items, totalCount, parm.PageNumber, parm.Take);
    }
}