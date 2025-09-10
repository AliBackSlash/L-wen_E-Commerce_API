namespace Löwen.Domain.Layer_Dtos.AppUser.response;

public class GetUserResponseDto
{
    public required string UserName { get; set; }
    public required string FName { get; set; }
    public string? MName { get; set; }
    public required string LName { get; set; }
    public string? PhoneNumber { get; set; }
    public char Gender {  get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ImagePath { get; set; }
    public DateTime? MarkedAsDeletedAt {  get; set; }
}
