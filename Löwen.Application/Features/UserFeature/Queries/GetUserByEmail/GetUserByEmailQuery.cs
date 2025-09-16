namespace Löwen.Application.Features.UserFeature.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string email) : ICommand<GetUserByEmailQueryResponse>;

