namespace DrakarVpn.Domain.Models.Pagination;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }

}

