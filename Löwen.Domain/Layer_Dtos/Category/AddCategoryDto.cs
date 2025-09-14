namespace Löwen.Domain.Layer_Dtos.Category;
public class AddCategoryDto
{
    public string? CategoryName { get; set; }
    public char Gender { get; set; }
    public byte AgeFrom { get; set; }
    public byte AgeTo { get; set; }
}
