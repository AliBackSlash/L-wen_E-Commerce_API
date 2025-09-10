namespace Löwen.Domain.Layer_Dtos.AppUser.response;

public class GetUserByIdResponseDto
{
    public string? FName { get; set; }
    public string? MName { get; set; }
    public string? LName { get; set; }
    public string? PhoneNumber { get; set; }
    public char Gender {  get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ImagePath { get; set; }
}
