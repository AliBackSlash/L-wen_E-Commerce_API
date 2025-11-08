using Löwen.Application.Features.DiscountFeature.Queries.Response;
using Löwen.Application.Messaging.ICommand;

namespace Löwen.Application.Features.DiscountFeature.Queries.GetById;

public record GetDiscountByIdQuery(string Id) : ICommand<DiscountResponse>;