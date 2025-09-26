using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Order;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class OrderService(AppDbContext _context) : BasRepository<Order, Guid>(_context), IOrderService
{
    public async Task<Result<OrderDetailsDto>> GetOrderDetails(Guid Id, CancellationToken ct)
    {
        var or_details = from order in _context.Orders
                         join items in _context.OrderItems on order.Id equals items.OrderId into items

                         where order.Id == Id
                         select new
                         {
                             
                             order.OrderDate,
                             order.Status,
                             items = items.Select(item => new 
                             {
                                 item.ProductId,
                                 item.Quantity,
                                 item.PriceAtPurchase,
                             })
                         };
        var s = or_details.ToQueryString();

        OrderDetailsDto? orderDetails = await or_details.Select(od => new OrderDetailsDto()
        {
            OrderDate = od.OrderDate,
            Status = od.Status,
            items = od.items.Select(item => new OrderDetailsItems
            {
               ProductId = item.ProductId,
               Quantity = item.Quantity,
               PriceAtPurchase = item.PriceAtPurchase
            }).AsEnumerable()
        }).FirstOrDefaultAsync();


        if (orderDetails is null)
            return Result.Failure<OrderDetailsDto>(new Error("IOrderService.GetOrderDetails", "no order details with order id {Id}", ErrorType.Conflict));

        return Result.Success(orderDetails);

    }

    public async Task<bool> IsFound(Guid Id, CancellationToken ct) => await _context.Orders.AnyAsync(t => t.Id == Id, ct);
}

