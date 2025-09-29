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

    public static GetUsersQueryResponse Map(GetUsersResponseDto dto)
    {
        return new GetUsersQueryResponse
        {
            Id = dto.Id, 
            UserName = dto.UserName,
            Name = dto.FName + (string.IsNullOrWhiteSpace(dto.MName) ? " " : $" {dto.MName} ") + dto.LName,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender,
            IsActive = dto.IsActive,
        };
    }
    public static IEnumerable<GetUsersQueryResponse> Map(IEnumerable<GetUsersResponseDto> dto)
    {
        if(dto is null) return [];

        return dto.Select(d => Map(d)).ToList();
    }
}
