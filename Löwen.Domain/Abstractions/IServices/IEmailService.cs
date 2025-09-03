using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices;

public interface IEmailService
{
    Task<Result> SendVerificationEmailAsync(string To, string Token, CancellationToken cancellationToken, string Subject = "Confirm your account");
    Task<Result<int>> SendOTPCodeAsync(string To, CancellationToken cancellationToken, string Subject = "Löwen – Your Password Reset Code");
    Task<Result<int>> SendRestPasswordTokenAsync(string To, string token, CancellationToken cancellationToken, string Subject = "Löwen – Reset Your Password");

}
