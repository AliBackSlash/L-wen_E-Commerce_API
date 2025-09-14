using Löwen.Domain.ErrorHandleClasses;

namespace Löwen.Domain.Abstractions.IServices.IEmailServices;

public interface IEmailService
{
    Task<Result> SendVerificationEmailAsync(string To, string Token, CancellationToken ct, string Subject = "Confirm your account");
    Task<Result<int>> SendOTPCodeAsync(string To, CancellationToken ct, string Subject = "Löwen – Your Password Reset Code");
    Task<Result<int>> SendRestPasswordTokenAsync(string To, string token, CancellationToken ct, string Subject = "Löwen – Reset Your Password");

}
