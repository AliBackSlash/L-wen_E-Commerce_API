using Löwen.Application.Features.DiscountFeature.Queries.Response;

namespace Löwen.Application.Features.DiscountFeature.Queries.GetAll;

public record GetAllDiscountQuery(int PageNumber, byte PageSize) : ICommand<PagedResult<DiscountResponse>>;