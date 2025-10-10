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
    public async Task<Result> AssignedOrdersToDelivery(IEnumerable<DeliveryOrder> dto, CancellationToken ct)
    {
        try
        {
            await _context.DeliveryOrders.AddRangeAsync(dto);

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
                                     Path = (from pi in _context.Images
                                             join img in _context.Images on pi.Id equals img.Id
                                             //where pi.ProductId == item.ProductId
                                             orderby img.IsMain descending
                                             select img.Path).FirstOrDefault()
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
                                     Path = (from pi in _context.Images
                                             join img in _context.Images on pi.Id equals img.Id
                                          //   where pi.ProductId == item.ProductId
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
                    where order.DeliveryId == userId
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
                                     Path = (from pi in _context.Images
                                             join img in _context.Images on pi.Id equals img.Id
                                            // where pi.ProductId == item.ProductId
                                             orderby img.IsMain descending
                                             select img.Path).FirstOrDefault()
                                 }).ToList()
                    };
        var totalCount = await query.CountAsync();
        var orders = await query.Skip(parm.Skip).Take(parm.Take).ToListAsync(ct);

        if (orders is null || !orders.Any())
            return Result.Failure<PagedResult<OrderDetailsDto>>(
                new Error("IOrderService.GetOrdersForUser", $"no orders for user {userId}", ErrorType.Conflict));

        return Result.Success<PagedResult<OrderDetailsDto>>(PagedResult<OrderDetailsDto>.Create(orders, totalCount, parm.PageNumber, parm.Take));
    }
    

    public async Task<bool> IsFound(Guid Id, CancellationToken ct) => await _context.Orders.AnyAsync(t => t.Id == Id, ct);
}

public class PaymentService(AppDbContext context) : IPaymentService
{
    private readonly DbSet<Payment> _db = context.Set<Payment>();
    public async Task<Result> AddAsync(Payment payment, CancellationToken ct)
    {
        try
        {
            await _db.AddAsync(payment);
            await context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"IPaymentService.AddAsync", ex.Message, ErrorType.InternalServer));
        }
    }

    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken ct) => await _db.FindAsync(id, ct);

    public async Task<Payment?> GetByTransactionId(string TransactionId, CancellationToken ct)
        => await _db.Where(x => x.TransactionId == TransactionId).FirstOrDefaultAsync(ct);

    public async Task<Result> UpdateStatus(Guid id, PaymentStatus status, CancellationToken ct)
    {
        Payment? payment = await _db.FindAsync(id, ct);
        if (payment is null)
            return Result.Failure(new Error("IPaymentService.UpdateStatus", "payment with Id {id} not found", ErrorType.Conflict));

        try
        {
            payment.Status = status;

            _db.Update(payment);
            await context.SaveChangesAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error($"IPaymentService.UpdateStatus", ex.Message, ErrorType.InternalServer));
        } 
    }

    }