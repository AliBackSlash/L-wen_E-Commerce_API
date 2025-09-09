namespace Löwen.Domain.Layer_Dtos.AppUser.request;

public record UpdateUserInfoDto(string id,string? FName, string? MName, string? LName,DateOnly? DateOfBirth, string? phoneNumber, char? Gender);
