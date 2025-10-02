using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEntitiesServices;

public interface IPaymentService
{
    Task<Payment> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Payment> GetByTransactionId(string TransactionId, CancellationToken ct);
    Task<Result> AddAsync(Payment payment, CancellationToken ct);
    Task<Result> UpdateStatus(Guid Id,PaymentStatus status, CancellationToken ct);

}