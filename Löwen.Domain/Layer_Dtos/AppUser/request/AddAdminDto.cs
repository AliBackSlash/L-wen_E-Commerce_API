namespace Löwen.Domain.Layer_Dtos.AppUser.request;

public record AddAdminDto(string Email, string UserName, string Password,string? FName,
    string? MName, string? LName, DateOnly DateOfBirth, string? phoneNumber, char Gender);

