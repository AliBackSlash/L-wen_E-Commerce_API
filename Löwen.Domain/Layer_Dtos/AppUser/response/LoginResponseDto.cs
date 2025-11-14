namespace Löwen.Domain.Layer_Dtos.AppUser.response;

public class LoginResponseDto
{
    public required string accessToken {  get; set; }
    public required string refreshToken {  get; set; }

}
