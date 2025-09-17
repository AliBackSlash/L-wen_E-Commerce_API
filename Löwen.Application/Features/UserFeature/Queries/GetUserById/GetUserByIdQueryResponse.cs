namespace Löwen.Application.Features.UserFeature.Queries.GetUserById;

public class GetUserByIdQueryResponse
{
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public char Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ImagePath { get; set; }
}
