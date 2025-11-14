namespace Löwen.Domain.Layer_Dtos.AppUser.request;

public record LoginDto(string UserNameOrEmail, string? Password)
{
    public string DeviceName { get; set; }
    public string IpAddress { get; set; }
}
