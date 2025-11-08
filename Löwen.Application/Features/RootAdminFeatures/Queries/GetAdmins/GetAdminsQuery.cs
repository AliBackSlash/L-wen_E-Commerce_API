using Löwen.Application.Messaging.IQuery;

namespace Löwen.Application.Features.RootAdminFeatures.Queries.GetAdmins;
public record GetAdminsQuery(UserRole Role) : IQuery<List<GetdminsQueryResponse>>;
