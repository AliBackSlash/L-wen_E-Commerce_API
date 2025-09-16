namespace Löwen.Application.Features.UserFeature.Queries.GetUserById;

public record GetUserByIdQuery(string Id) : ICommand<GetUserByIdQueryResponse>;

