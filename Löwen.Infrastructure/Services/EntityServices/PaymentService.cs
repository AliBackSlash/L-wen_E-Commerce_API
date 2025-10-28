using Löwen.Domain.Abstractions.IServices.IEntitiesServices;
using Löwen.Domain.Enums;
using Löwen.Domain.ErrorHandleClasses;
using Microsoft.EntityFrameworkCore;

namespace Löwen.Infrastructure.Services.EntityServices;

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