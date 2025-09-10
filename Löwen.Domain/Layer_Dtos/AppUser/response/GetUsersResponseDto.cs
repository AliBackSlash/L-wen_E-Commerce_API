namespace Löwen.Domain.Layer_Dtos.AppUser.response;

public class GetUsersResponseDto
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string FName { get; set; }
    public string? MName { get; set; }
    public required string LName { get; set; }
    public string? PhoneNumber { get; set; }
    public char Gender {  get; set; }
    public bool IsActive {  get; set; }
}
