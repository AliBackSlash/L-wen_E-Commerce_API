namespace Löwen.Application.Features.UserFeature.Queries;

public record GetUserByIdQuery(string token) : ICommand<GetUserByIdQueryResponse>;

