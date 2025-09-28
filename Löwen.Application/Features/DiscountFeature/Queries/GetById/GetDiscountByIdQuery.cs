using Löwen.Application.Features.DiscountFeature.Queries.Response;

namespace Löwen.Application.Features.DiscountFeature.Queries.GetById;

public record GetDiscountByIdQuery(string Id) : ICommand<DiscountResponse>;