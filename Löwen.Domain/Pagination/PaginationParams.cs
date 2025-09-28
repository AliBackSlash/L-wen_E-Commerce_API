using Löwen.Domain.ConfigurationClasses.Pagination;

namespace Löwen.Domain.Pagination;

public class PaginationParams
{
    public short maxPageSize {  get; set; }
    public int PageNumber { get; set; } = 1;
    private int _pageSize;
    public int Take
    {
        get { return _pageSize; }
        set { _pageSize = Math.Clamp(value, 1, maxPageSize); }
    }

    public int Skip => (PageNumber - 1) * Take;
}
