namespace Löwen.Presentation.Api.Controllers.v1.DiscountController.Models;

public record AddDiscountModel(string Name, DiscountType DiscountType,
    decimal? DiscountValue, DateTime StartDate, DateTime EndDate, bool IsActive);
