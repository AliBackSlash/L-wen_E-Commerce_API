using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;
public record GetAdminByIdQuery(Guid Id,UserRole Role) : IQuery<GetUserQueryResponse>;
