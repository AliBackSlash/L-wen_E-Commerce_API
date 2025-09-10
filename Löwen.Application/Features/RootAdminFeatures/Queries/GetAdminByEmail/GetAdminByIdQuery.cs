namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminByEmail;
public record GetAdminByIdQuery(Guid Id,UserRole Role) : IQuery<GetAdminByIdQueryResponse>;
