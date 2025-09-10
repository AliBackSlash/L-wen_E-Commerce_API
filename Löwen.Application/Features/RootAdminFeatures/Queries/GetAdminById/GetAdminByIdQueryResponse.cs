namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;
public class GetAdminByIdQueryResponse
{
    public string? FName { get; set; }
    public string? MName { get; set; }
    public string? LName { get; set; }
    public string? PhoneNumber { get; set; }
    public char Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ImagePath { get; set; }

    private GetAdminByIdQueryResponse() { }

    public static GetAdminByIdQueryResponse Map(GetUserByIdResponseDto dto)
    {
        return new GetAdminByIdQueryResponse
        {
            FName = dto.FName,
            MName = dto.MName,
            LName = dto.LName,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender,
            ImagePath = dto.ImagePath,
            DateOfBirth = dto.DateOfBirth,
        };
    }
}
