using Löwen.Domain.Abstractions.IServices;
using Löwen.Domain.ErrorHandleClasses;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Löwen.Infrastructure.Services.EmailServices;

public class EmailService(IConfiguration _config) : IEmailService
{
    private async Task<Result> SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = new SmtpClient
            {
                Host = _config["EmailSettings:SmtpServer"]!,
                Port = int.Parse(_config["EmailSettings:Port"]!),
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    _config["EmailSettings:Username"],
                    _config["EmailSettings:Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:FromEmail"]!),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage, cancellationToken);

            return Result.Success(); 
        }
        catch (SmtpFailedRecipientException ex)
        {
            return Result.Failure(new Error("(Send Email) Invalid recipient", ex.FailedRecipient!, ErrorType.Failure));
        }
        catch (SmtpException ex)
        {
            return Result.Failure(new Error("(Send Email) SMTP Error", ex.Message, ErrorType.Failure));
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("(Send Email) Error accorded when send email", ex.Message, ErrorType.Failure));
        }
    }
    private async Task<Result<string>> PrepareHTMLBodyAsync(string  folder,string file,string replaceFrom,string replaceTo)
    {
        try
        {
            var htmlBody = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), folder, file));

            return  Result.Success(htmlBody.Replace(replaceFrom, replaceTo));
        }
        catch (Exception ex)
        {
            return Result.Failure<string>(Error.InternalServer("Read Templete  Error",$"Error Message: {ex.Message}"));
        }
       
    }

    public async Task<Result<int>> SendOTPCodeAsync(string To, CancellationToken cancellationToken, string Subject = "Löwen – Your Password Reset Code")
    {
        int OTPCode = Random.Shared.Next(100000, 999999);
        var result = await PrepareHTMLBodyAsync("EmailTemplates", "OTPCode.html", "{{otpCode}}", OTPCode.ToString());

        if (result.IsFailure)
            return Result.Failure<int>(result.Errors);

        var sendResult = await SendEmailAsync(To, Subject, result.Value, cancellationToken);

        if (sendResult.IsFailure)
            return Result.Failure<int>(sendResult.Errors);

        return Result.Success(OTPCode);
    }
    public async Task<Result<int>> SendRestPasswordTokenAsync(string To,string token, CancellationToken cancellationToken, string Subject = "Löwen – Reset Your Password")
    {
        int OTPCode = Random.Shared.Next(100000, 999999);
        var result = await PrepareHTMLBodyAsync("EmailTemplates", "ChangePassword.html", "{{resetPasswordLink}}", token);

        if (result.IsFailure)
            return Result.Failure<int>(result.Errors);

        var sendResult = await SendEmailAsync(To, Subject, result.Value, cancellationToken);

        if (sendResult.IsFailure)
            return Result.Failure<int>(sendResult.Errors);
        
        return Result.Success(OTPCode);
    }
    public async Task<Result> SendVerificationEmailAsync(string To, string Token, CancellationToken cancellationToken, string Subject = "Confirm your account")
    {

        var result = await PrepareHTMLBodyAsync("EmailTemplates", "ConfirmEmail.html", "{{confirmationLink}}", Token);

        if (result.IsFailure)
            return result;

        return await SendEmailAsync(To, Subject, result.Value, cancellationToken);
    }
}
