namespace Löwen.Presentation.Api.Controllers.v1.DiscountController.Models;

public record UpdateDiscountModel(string Id,string? Name, DiscountType? DiscountType,
    decimal? DiscountValue, DateTime? StartDate, DateTime? EndDate, bool? IsActive);