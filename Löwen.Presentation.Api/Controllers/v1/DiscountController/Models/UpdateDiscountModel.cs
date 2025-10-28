namespace Löwen.Presentation.Api.Controllers.v1.DiscountController.Models;

public record UpdateDiscountModel(string Id,string? Name, DiscountType? DiscountType,
    double? DiscountValue, DateTime? StartDate, DateTime? EndDate, bool? IsActive);