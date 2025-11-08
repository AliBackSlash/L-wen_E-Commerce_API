using Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminById;
using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdminByEmail;
public record GetAdminByEmailQuery(string email,UserRole Role) : IQuery<GetUserQueryResponse>;
