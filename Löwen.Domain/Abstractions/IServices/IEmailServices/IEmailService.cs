using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEmailServices;

public interface IEmailService
{
    Task<Result> SendVerificationEmailAsync(string To, string Token, CancellationToken ct, string Subject = "Löwen – تأكيد حسابك");
    Task<Result<int>> SendOTPCodeAsync(string To, CancellationToken ct, string Subject = "Löwen – رمز إعادة تعيين كلمة المرور");
    Task<Result<int>> SendRestPasswordTokenAsync(string To, string token, CancellationToken ct, string Subject = "Löwen – إعادة تعيين كلمة المرور");
    Task<Result> SendOrderStatusAsync(string To, OrderStatus status, string CustomerName, CancellationToken ct, string Subject = "Löwen – تحديث حالة الطلب");
}
