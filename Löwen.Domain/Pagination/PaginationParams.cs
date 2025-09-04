using Löwen.Domain.ConfigurationClasses.Pagination;

namespace Löwen.Domain.Pagination;

public class PaginationParams(PaginationSettings settings)
{
    public int PageNumber { get; set; } = 1;
    public int PageSize {
        get { return PageSize; }
        set
        {
            PageSize = Math.Clamp(value, 1, settings.maxPageSize);
        }
    
    }

    public int Skip => (PageNumber - 1) * PageSize;
}
