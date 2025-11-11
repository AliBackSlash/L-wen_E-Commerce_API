using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
using Löwen.Domain.Enums;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Delivery;
using Löwen.Domain.Layer_Dtos.Order;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

public class OrderService(AppDbContext _context) : BasRepository<Order, Guid>(_context), IOrderService
{
    public async Task<Result> AssignedOrdersToDelivery(IEnumerable<DeliveryOrder> d_o, CancellationToken ct)
    {
        try
        {
            
            await _context.DeliveryOrders.AddRangeAsync(d_o);
            var orders = await _dbSet.Where(x => d_o.Select(x => x.OrderId).Contains(x.Id)).ToListAsync();
           
            foreach (var order in orders)
                order.Status = OrderStatus.Shipped;

            _context.UpdateRange(orders);
            await _context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"IOrderService.AssignedOrdersToDelivery", ex.Message, ErrorType.InternalServer));
        }
    }
    public async Task<Result<PagedResult<OrderDetailsDto>>> GetAllOrders(PaginationParams parm, CancellationToken ct)
    {
        var query = from order in _context.Orders
                    orderby order.OrderDate ascending
                    select new OrderDetailsDto
                    {
                        Id = order.Id,
                        OrderDate = order.OrderDate,
                        Status = order.Status,
                        items = (from orItems in _context.OrderItems
                                 join product in _context.Products on orItems.ProductId equals product.Id
                                 where orItems.OrderId == order.Id
                                 select new OrderDetailsItemsDto
                                 {
                                     ProductId = orItems.ProductId,
                                     ProductName = product.Name,
                                     Quantity = orItems.Quantity,
                                     Price = orItems.Price,
                                     Path = _context.Images.Where(x => x.ProductId == product.Id && x.IsMain == true).Select(x => x.Path).Take(1).Single()!

                                 }).ToList()

                    };
        var totalCount = await query.CountAsync();
        var orders = await query.Skip(parm.Skip).Take(parm.Take).ToListAsync(ct);
     
        return Result.Success<PagedResult<OrderDetailsDto>>(PagedResult<OrderDetailsDto>.Create(orders, totalCount, parm.PageNumber, parm.Take));
    }  
    public async Task<Result<OrderDetailsDto>> GetOrderDetails(Guid Id, CancellationToken ct)
    {

        var query = from order in _context.Orders 
                    where order.Id == Id
                    orderby order.OrderDate descending
                    select new OrderDetailsDto
                    {
                        OrderDate = order.OrderDate,
                        Status = order.Status,
                        items = ( from orItems in _context.OrderItems 
                                  join product in _context.Products on orItems.ProductId equals product.Id
                                  where orItems.OrderId == order.Id 
                                  select new OrderDetailsItemsDto
                                  {
                                      ProductId = orItems.ProductId,
                                      ProductName = product.Name,
                                      Quantity = orItems.Quantity,
                                      Price = orItems.Price,
                                      Path = _context.Images.Where(x => x.ProductId == product.Id && x.IsMain == true).Select(x => x.Path).Take(1).Single()!
                                              
                                  }).ToList()

                    };
        var q = query.ToQueryString();
        var orderDetails = await query.AsNoTracking().FirstOrDefaultAsync(ct);


        return Result.Success(orderDetails)!;
    }
    public async Task<Result<PagedResult<OrderDetailsDto>>> GetOrdersForUser(Guid userId,PaginationParams parm, CancellationToken ct)
    {
        var query = from order in _context.Orders
                    where order.CustomerId == userId
                    orderby order.OrderDate descending
                    select new OrderDetailsDto
                    {
                        Id = order.Id,
                        OrderDate = order.OrderDate,
                        Status = order.Status,
                        items = (from orItems in _context.OrderItems
                                 join product in _context.Products on orItems.ProductId equals product.Id
                                 where orItems.OrderId == order.Id
                                 select new OrderDetailsItemsDto
                                 {
                                     ProductId = orItems.ProductId,
                                     ProductName = product.Name,
                                     Quantity = orItems.Quantity,
                                     Price = orItems.Price,
                                     Path = _context.Images.Where(x => x.ProductId == product.Id && x.IsMain == true).Select(x => x.Path).Take(1).Single()!

                                 }).ToList()

                    };
        var totalCount = await query.CountAsync();
        var orders = await query.Skip(parm.Skip).Take(parm.Take).ToListAsync(ct);


        return Result.Success<PagedResult<OrderDetailsDto>>(PagedResult<OrderDetailsDto>.Create(orders, totalCount, parm.PageNumber, parm.Take));
    }
    public async Task<bool> IsFound(Guid Id, CancellationToken ct) => await _context.Orders.AnyAsync(t => t.Id == Id, ct);
}
