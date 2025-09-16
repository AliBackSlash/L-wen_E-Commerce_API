namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdmins;
public class GetdminsQueryResponse
{

    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public char Gender { get; set; }
    public bool? IsActive { get; set; }
    private GetdminsQueryResponse() { }

    public static GetdminsQueryResponse Map(GetUsersResponseDto dto)
    {
        if (dto is null)
            return null!;

        return new GetdminsQueryResponse
        {
            Id = dto.Id,
            FullName = dto.FName + (string.IsNullOrWhiteSpace(dto.MName) ? " " : $" {dto.MName} ") + dto.LName,
            UserName = dto!.UserName,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender,
            IsActive = dto.IsActive,
            
        };
    }

    public static List<GetdminsQueryResponse> Map(List<GetUsersResponseDto> dtos)
    {
        if (dtos is null)
            return [];


        return dtos.Select(d => Map(d)).ToList();
    }
}
