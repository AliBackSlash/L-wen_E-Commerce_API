namespace Löwen.Application.Features.ProductFeature.Queries.GetAllProductPagedByGender;

public record GetAllProductPagedByGenderQuery(char Gender,int PageNumber,byte PageSize) : IQuery<PagedResult<GetProductQueryResponse>>;
