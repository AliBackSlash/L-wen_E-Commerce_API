using Löwen.Domain.Enums;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.AppUser.request;
using Löwen.Domain.Layer_Dtos.AppUser.response;

namespace Löwen.Domain.Abstractions.IServices.IAppUserServices;
public interface IAppUserService
{
    Task<Result<RegisterResponseDto>> RegisterAsync(RegisterUserDto dto, CancellationToken ct);
    Task<Result<GetUserResponseDto>> GetUserByIdAsync(string id,UserRole role = UserRole.User);
    Task<Result<List<GetUsersResponseDto>>> GetAllAsync(UserRole role = UserRole.User);
    Task<Result<GetUserResponseDto>> GetUserByEmailAsync(string email,UserRole role = UserRole.User);
    Result<string> GetUserIdFromToken(string token);
    Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto, CancellationToken ct);
    Task<Result<UpdateUserInfoResponseDto>> UpdateUserInfoAsync(UpdateUserInfoDto dto);
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
