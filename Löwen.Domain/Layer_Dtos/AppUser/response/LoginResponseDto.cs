namespace Löwen.Domain.Layer_Dtos.AppUser.response;

public record LoginResponseDto(string Token,string Email, string UserName,List<string> Roles, DateTime Expiration);
