using Löwen.Application.Abstractions.IServices.IdentityServices;
using Löwen.Domain.ConfigurationClasses.ApiSettings;
using Löwen.Domain.ConfigurationClasses.JWT;
using Löwen.Domain.ConfigurationClasses.StaticFilesHelpersClasses;
using Löwen.Domain.Enums;
using Löwen.Domain.ErrorHandleClasses;
using Löwen.Domain.Layer_Dtos.AppUser.request;
using Löwen.Domain.Layer_Dtos.AppUser.response;
using Löwen.Infrastructure.EFCore.IdentityUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Löwen.Infrastructure.Services.IdentityServices;
public class AppUserService(UserManager<AppUser> _userManager, IOptions<JWT> _jwt, IOptions<ApiSettings> apiSettings) : IAppUserService
{

    public async Task<Result<RegisterResponseDto>> RegisterAsync(RegisterUserDto reg_info, CancellationToken cancellationToken)
    {
       
        AppUser user = new()
        {
            UserName = reg_info.UserName,
            Email = reg_info.Email
        };

        var createResult = await _userManager.CreateAsync(user, reg_info.Password);
        if (!createResult.Succeeded)
            return Result.Failure<RegisterResponseDto>(new Error("Create Errors", string.Join(", ", createResult.Errors.Select(e => e.Description)), ErrorType.Create));

        return Result.Success(new RegisterResponseDto(user.Id, await _CreateJWTToken(user)));
    }
    private async Task<string> _CreateJWTToken(AppUser appUser)
    {
        var userClaims = await _userManager.GetClaimsAsync(appUser);
        var roles = await _userManager.GetRolesAsync(appUser);
        var roleClaims = roles.Select(r => new Claim("roles", r)).ToList();

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName ?? ""),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, appUser.Email ?? ""),
        new Claim(JwtRegisteredClaimNames.UniqueName, appUser.UserName ?? ""),
        new Claim(JwtRegisteredClaimNames.NameId, appUser.Id.ToString()),
    };

        claims.AddRange(userClaims);
        claims.AddRange(roleClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Value.Issuer,
            audience: _jwt.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.Value.Duration),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private Task<(string? Email, string? UserName, List<string> Roles, DateTime Expiration)> _GetInfoFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userName = jwtToken.Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName)?.Value;

        var email = jwtToken.Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;

        var roles = jwtToken.Claims
            .Where(c => c.Type == "roles")
            .Select(c => c.Value)
            .ToList();

        var expiration = jwtToken.ValidTo;

        return Task.FromResult((email, userName, roles, expiration));
    }
    public Result<string> GetUserIdFromToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value;

            if (userId == null)
                return Result.Failure<string>(new Error("Invalid Token", $"Can't take user id from token",ErrorType.Validation));

            return Result.Success(userId);
        }
        catch (Exception ex)
        {

            return Result.Failure<string>(new Error("Invalid Token", $"Message: {ex}", ErrorType.Validation));
        }
    }
    public async Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail)
                   ?? await _userManager.FindByNameAsync(dto.UserNameOrEmail);

        if (user is null)
            return Result.Failure<LoginResponseDto>(
             new Error("User.InvalidCredentials", "Invalid username or password", ErrorType.Unauthorized));            

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return Result.Failure<LoginResponseDto>(
                new Error("User.EmailNotConfirmed", "You must confirm your email before logging in", ErrorType.Unauthorized));

        if (await _userManager.CheckPasswordAsync(user, dto.Password!))
        {
            string token = await _CreateJWTToken(user);
            var tokenInfo = await _GetInfoFromToken(token);
            return Result.Success(new LoginResponseDto(token, tokenInfo.Email!, tokenInfo.UserName!, tokenInfo.Roles, tokenInfo.Expiration));
        }

        return Result.Failure<LoginResponseDto>(
            new Error("User.InvalidCredentials", "Invalid username or password", ErrorType.Unauthorized));
    }
    public async Task<Result<string>> ConfirmEmailAsync(string userId,string token)
    {

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
           return Result.Failure<string>(Error.NotFound("Not Found", $"User with id {userId} not found"));
      
        try
        {
            var ConfirmResult = await _userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)));

            if (!ConfirmResult.Succeeded)
                return Result.Failure<string>(new Error("Confirm Email Errors", string.Join(", ", ConfirmResult.Errors.Select(e => e.Description)), ErrorType.Create));

            return Result.Success(await _CreateJWTToken(user));
        }
        catch (Exception)
        {
            return Result.Failure<string>(new Error("Invalid Token", "The input is not a valid Base-64 string as it contains a non-base 64 character", ErrorType.Validation));
        }

    }
    public async Task<Result<string>> GenerateEmailConfirmationTokenAsync(string email)
    {
        AppUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Result.Failure<string>(new Error("(Generate Email Confirmation) Not Found", "", ErrorType.NotFound));

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));


        if (string.IsNullOrEmpty(encodedToken))
              return Result.Failure<string>(new Error("(Generate Email Confirmation) Error", "", ErrorType.NotFound));

        return Result.Success($"{apiSettings.Value.BaseUrl}/confirm-email?userId={user.Id}&token={encodedToken}");
    }
    public async Task<Result<bool>> IsEmailNotTakenAsync(string email)
    {
        if (await _userManager.FindByEmailAsync(email) is not null)
            return Result.Failure<bool>(new Error("Validation error", "Invalid UserName Or Email", ErrorType.Validation));
        return Result.Success(true);
    }
    public async Task<Result<bool>> IsUserNameNotTakenAsync(string userName)
    {
        if (await _userManager.FindByNameAsync(userName) is not null)
            return Result.Failure<bool>(new Error("Validation error", "Invalid UserName Or Email", ErrorType.Validation));

        return Result.Success(true);
    }
    public async Task<Result> AssignUserToRoleAsync(Guid userId, UserRole role)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure(new Error("(Add Role) Not Found", "", ErrorType.NotFound));

        var roleResult = await _userManager.AddToRoleAsync(user, role.ToString());
        if (!roleResult.Succeeded)
        {
            return Result.Failure<RegisterResponseDto>(new Error("Role Errors", string.Join(", ", roleResult.Errors.Select(e => e.Description)), ErrorType.Conflict));
        }

        return Result.Success();

    }
    public async Task<Result> RemoveRoleFromUserAsync(Guid userId, UserRole role)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure(new Error("(Remove Role)", "Not Found", ErrorType.NotFound));

        if(!await _userManager.IsInRoleAsync(user,role.ToString()))
            return Result.Failure(new Error("(Remove Role)", "User not have this role", ErrorType.NotFound));


        var roleResult = await _userManager.RemoveFromRoleAsync(user, role.ToString());
        if (!roleResult.Succeeded)
        {
            return Result.Failure<RegisterResponseDto>(new Error("Role Errors", string.Join(", ", roleResult.Errors.Select(e => e.Description)), ErrorType.Conflict));
        }

        return Result.Success();

    }
    public async Task<Result> MarkAsDeletedAsync(Guid userId)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure<bool>(new Error("(Mark As Deleted)", "Not Found", ErrorType.NotFound));
        
        if (user.IsDeleted)
            return Result.Failure(new Error("(Activate Marked As Deleted)", "User already marked as deleted", ErrorType.NotFound));

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return Result.Failure(new Error("(Mark As Deleted)", string.Join(", ", updateResult.Errors.Select(e => e.Description)), ErrorType.Create));

        return Result.Success();
    }
    public async Task<Result> ActivateMarkedAsDeletedAsync(Guid userId)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure(new Error("(Activate Marked As Deleted)", "Not Found", ErrorType.NotFound));

        if(!user.IsDeleted)
            return Result.Failure(new Error("(Activate Marked As Deleted)", "User already Active", ErrorType.NotFound));

        user.IsDeleted = false;
        user.DeletedAt = null;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return Result.Failure(new Error("(Activate Marked As Deleted)", string.Join(", ", updateResult.Errors.Select(e => e.Description)), ErrorType.Create));

        return Result.Success();
    }
    public async Task<Result<bool>> RemoveUserAsync(Guid userId)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure<bool>(new Error("(Delete User)", "Not Found", ErrorType.NotFound));
        var deleteResult = await _userManager.DeleteAsync(user);
        if(deleteResult.Succeeded)
            return Result.Success(true);

        return Result.Failure<bool>(new Error("(Delete User) Errors", string.Join(", ", deleteResult.Errors.Select(e => e.Description)), ErrorType.Delete));
    }
    public async Task<Result<string>> ResetPasswordAsync(string Email,string token, string Password)
    {
        var user = await  _userManager.FindByEmailAsync(Email);
        if (user is null)
            return Result.Failure<string>(Error.NotFound("Not Found", $"User with email {Email} not found"));

        try
        {
            var ChangeResult = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)), Password);

            if (!ChangeResult.Succeeded)
                return Result.Failure<string>(new Error("Reset password Errors", string.Join(", ", ChangeResult.Errors.Select(e => e.Description)), ErrorType.Create));

            return Result.Success(await _CreateJWTToken(user));
        }
        catch (Exception  ex)
        {
            return Result.Failure<string>(new Error("Reset password Error", ex.Message, ErrorType.Validation));
        }

    }
    public async Task<Result<string>> GenerateRestPasswordTokenAsync(string email)
    {
        AppUser? user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Result.Failure<string>(new Error("(Generate Rest Password) Not Found", "", ErrorType.NotFound));

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));


        if (string.IsNullOrEmpty(encodedToken))
            return Result.Failure<string>(new Error("(Generate Email Confirmation) Error", "", ErrorType.NotFound));
       
        return Result.Success($"{apiSettings.Value.BaseUrl}/confirm-email?userId={user.Id}&token={encodedToken}");
    }
    public async Task<Result<string>> ChangePasswordAsync(string Id,string CurrentPassword, string Password)
    {
        var user = await _userManager.FindByIdAsync(Id);
        if (user is null)
            return Result.Failure<string>(Error.NotFound("Not Found", $"User with Id {Id} not found"));

        try
        {
            var ChangeResult = await _userManager.ChangePasswordAsync(user, CurrentPassword, Password);

            if (!ChangeResult.Succeeded)
                return Result.Failure<string>(new Error("Change password Errors", string.Join(", ", ChangeResult.Errors.Select(e => e.Description)), ErrorType.Create));

            return Result.Success(await _CreateJWTToken(user));
        }
        catch (Exception ex)
        {
            return Result.Failure<string>(new Error("Change password Error", ex.Message, ErrorType.Validation));
        }
    }
    public async Task<Result> UpdateProfileImageAsync(string userId, string path, string rootPath)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure(Error.NotFound("Not Found", $"User with id {userId} not found"));

        var deleteResult = PicturesChecker.RemoveOldPictureIfExists(Path.Combine(rootPath, user.ImagePath ?? "no path"));
        if (deleteResult.IsFailure)
            return Result.Failure(deleteResult.Errors);

        user.ImagePath = path;

        await _userManager.UpdateAsync(user);
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return Result.Failure<UpdateUserInfoResponseDto>(new Error("Update Errors", string.Join(", ", updateResult.Errors.Select(e => e.Description)), ErrorType.Create));

        return Result.Success();

        throw new NotImplementedException();
    }
    public async Task<Result<UpdateUserInfoResponseDto>> UpdateUserInfo(UpdateUserInfoDto dto)
    {
        AppUser? user = await _userManager.FindByIdAsync(dto.id);
        if (user is null)
            return Result.Failure<UpdateUserInfoResponseDto>(Error.NotFound("Not Found", $"User with id {dto.id} not found"));

        user.FName = dto.FName ?? user.FName;
        user.MName = dto.MName ?? user.MName;
        user.LName = dto.LName ?? user.LName;
        user.PhoneNumber = dto.phoneNumber ?? user.PhoneNumber;
        user.DateOfBirth = dto.DateOfBirth ?? user.DateOfBirth;
        user.Gender = dto.Gender ?? user.Gender;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return Result.Failure<UpdateUserInfoResponseDto>(new Error("Update.Errors", string.Join(", ", updateResult.Errors.Select(e => e.Description)), ErrorType.Create));

        return Result.Success(new UpdateUserInfoResponseDto(await _CreateJWTToken(user)));

    }
    public async Task<Result<GetUserByIdResponseDto>> GetUserById(string id,UserRole role = UserRole.User)
    {
        var user = await _userManager.FindByIdAsync(id);
        
        if (user == null || !await _userManager.IsInRoleAsync(user,role.ToString()))
            return Result.Failure<GetUserByIdResponseDto>(new Error("(Get By Id)", "Not Found", ErrorType.NotFound));




        return Result.Success(new GetUserByIdResponseDto
        {
            FName = user.FName,
            MName = user.MName,
            LName = user.LName,
            Gender = user.Gender,
            DateOfBirth = user.DateOfBirth,
            ImagePath = user.ImagePath,
            PhoneNumber = user.PhoneNumber
        });
    }

    public async Task<Result<Guid>> AddAdminAsync(AddAdminDto dto)
    {
        AppUser user = new()
        {
            Email = dto.Email,
            UserName = dto.UserName,
            PhoneNumber = dto.phoneNumber,
            FName = dto.FName,
            MName = dto.MName,
            LName= dto.LName,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            

        };

        var createResult = await _userManager.CreateAsync(user, dto.Password);
        if (!createResult.Succeeded)
            return Result.Failure<Guid>(new Error("Create Errors", string.Join(", ", createResult.Errors.Select(e => e.Description)), ErrorType.Create));

        return Result.Success(user.Id);
    }
}

