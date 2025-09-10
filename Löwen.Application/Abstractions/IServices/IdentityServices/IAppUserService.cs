
using Löwen.Application.Features.UserFeature.Commands;
using Löwen.Application.Features.UserFeature.Queries;
using Löwen.Domain.Enums;

namespace Löwen.Application.Abstractions.IServices.IdentityServices;
public interface IAppUserService
{
    Task<Result<RegisterResponseDto>> RegisterAsync(RegisterUserDto dto, CancellationToken cancellationToken);
    Task<Result<GetUserByIdResponseDto>> GetUserById(string id,UserRole role = UserRole.User);
    Result<string> GetUserIdFromToken(string token);
    Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
    Task<Result<UpdateUserInfoResponseDto>> UpdateUserInfo(UpdateUserInfoDto dto);
    Task<Result<string>> ConfirmEmailAsync(string userId, string token);
    Task<Result<string>> ChangePasswordAsync(string Id, string CurrentPassword, string Password);
    Task<Result<bool>> IsEmailNotTakenAsync(string email);
    Task<Result<bool>> IsUserNameNotTakenAsync(string userName);
    Task<Result> AssignUserToRoleAsync(Guid userId, UserRole role);
    Task<Result> RemoveRoleFromUserAsync(Guid userId, UserRole role);
    Task<Result> MarkAsDeletedAsync(Guid userId);
    Task<Result> ActivateMarkedAsDeletedAsync(Guid userId);
    Task<Result<Guid>> AddAdminAsync(AddAdminDto dto);
    Task<Result<string>> GenerateEmailConfirmationTokenAsync(string email);
    Task<Result<string>> GenerateRestPasswordTokenAsync(string email);
    Task<Result<string>> ResetPasswordAsync(string Email, string token, string Password);
    Task<Result<bool>> RemoveUserAsync(Guid userId);
    Task<Result> UpdateProfileImageAsync(string userId,string path, string rootPath);
}
