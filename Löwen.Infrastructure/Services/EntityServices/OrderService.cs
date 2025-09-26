using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Entities;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.Order;
using Löwen.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using static Löwen.Domain.ErrorHandleClasses.ErrorCodes;

namespace Löwen.Infrastructure.Services.EntityServices;

public class OrderService(AppDbContext _context) : BasRepository<Order, Guid>(_context), IOrderService
{
    public async Task<Result<PagedResult<OrderDetailsDto>>> GetAllOrders(PaginationParams parm, CancellationToken ct)
    {
        var query = from order in _context.Orders
                    select new OrderDetailsDto
                    {
                        OrderDate = order.OrderDate,
                        Status = order.Status,
                        items = (from item in _context.OrderItems
                                 where item.OrderId == order.Id
                                 select new OrderDetailsItems
                                 {
                                     ProductId = item.ProductId,
                                     Quantity = item.Quantity,
                                     PriceAtPurchase = item.PriceAtPurchase,
                                     Path = (from pi in _context.ProductImages
                                             join img in _context.Images on pi.ImageId equals img.Id
                                             where pi.ProductId == item.ProductId
                                             orderby img.IsMain descending
                                             select img.Path).FirstOrDefault()
                                 }).ToList()
                    };
        var totalCount = await query.CountAsync();
        var orders = await query.Skip(parm.Skip).Take(parm.PageSize).ToListAsync(ct);
     
        return Result.Success<PagedResult<OrderDetailsDto>>(PagedResult<OrderDetailsDto>.Create(orders, totalCount, parm.PageNumber, parm.PageSize));
    }

    public async Task<Result<OrderDetailsDto>> GetOrderDetails(Guid Id, CancellationToken ct)
    {
        var query = from order in _context.Orders
                    where order.Id == Id
                    select new OrderDetailsDto
                    {
                        OrderDate = order.OrderDate,
                        Status = order.Status,
                        items = (from item in _context.OrderItems
                                 where item.OrderId == order.Id
                                 select new OrderDetailsItems
                                 {
                                     ProductId = item.ProductId,
                                     Quantity = item.Quantity,
                                     PriceAtPurchase = item.PriceAtPurchase,
                                     Path = (from pi in _context.ProductImages
                                             join img in _context.Images on pi.ImageId equals img.Id
                                             where pi.ProductId == item.ProductId
                                             orderby img.IsMain descending
                                             select img.Path).FirstOrDefault()
                                 }).ToList()
                    };

        var orderDetails = await query.FirstOrDefaultAsync(ct);

        if (orderDetails is null)
            return Result.Failure<OrderDetailsDto>(
                new Error("IOrderService.GetOrderDetails", $"no order details with order id {Id}", ErrorType.Conflict));

        return Result.Success(orderDetails);
    }


    public async Task<Result<PagedResult<OrderDetailsDto>>> GetOrdersForUser(Guid userId,PaginationParams parm, CancellationToken ct)
    {
        var query = from order in _context.Orders
                    where order.UserId == userId
                    select new OrderDetailsDto
                    {
                        OrderDate = order.OrderDate,
                        Status = order.Status,
                        items = (from item in _context.OrderItems
                                 where item.OrderId == order.Id
                                 select new OrderDetailsItems
                                 {
                                     ProductId = item.ProductId,
                                     Quantity = item.Quantity,
                                     PriceAtPurchase = item.PriceAtPurchase,
                                     Path = (from pi in _context.ProductImages
                                             join img in _context.Images on pi.ImageId equals img.Id
                                             where pi.ProductId == item.ProductId
                                             orderby img.IsMain descending
                                             select img.Path).FirstOrDefault()
                                 }).ToList()
                    };
        var totalCount = await query.CountAsync();
        var orders = await query.Skip(parm.Skip).Take(parm.PageSize).ToListAsync(ct);

        if (orders is null || !orders.Any())
            return Result.Failure<PagedResult<OrderDetailsDto>>(
                new Error("IOrderService.GetOrdersForUser", $"no orders for user {userId}", ErrorType.Conflict));

        return Result.Success<PagedResult<OrderDetailsDto>>(PagedResult<OrderDetailsDto>.Create(orders, totalCount, parm.PageNumber, parm.PageSize));
    }
    

    public async Task<bool> IsFound(Guid Id, CancellationToken ct) => await _context.Orders.AnyAsync(t => t.Id == Id, ct);
}

