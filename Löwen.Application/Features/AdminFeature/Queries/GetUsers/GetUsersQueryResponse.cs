using static Löwen.Domain.ErrorHandleClasses.ErrorCodes;

namespace Löwen.Application.Features.AdminFeature.Queries.GetUsers;

public class GetUsersQueryResponse
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public char Gender { get; set; }
    public bool IsActive { get; set; }

}
