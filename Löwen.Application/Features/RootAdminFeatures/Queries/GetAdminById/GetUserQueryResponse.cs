namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;
public class GetUserQueryResponse
{

    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public char Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ImagePath { get; set; }
    public DateTime? MarkedAsDatetedAt { get; set; }

    private GetUserQueryResponse() { }

    public static GetUserQueryResponse Map(GetUserResponseDto dto)
    {
        if (dto is null)
            return null!;

        return new GetUserQueryResponse
        {
            FullName = dto.FName +' '+ dto.MName != null ? dto?.MName + ' ' : "" + dto!.LName,
            UserName = dto!.UserName,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender,
            ImagePath = dto.ImagePath,
            DateOfBirth = dto.DateOfBirth,
            MarkedAsDatetedAt = dto.MarkedAsDeletedAt
            
        };
    }

}
