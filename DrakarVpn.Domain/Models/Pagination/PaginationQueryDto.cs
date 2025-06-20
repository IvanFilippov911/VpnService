namespace DrakarVpn.Domain.Models.Pagination;

public class PaginationQueryDto : IPaginatable
{
    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 50;
}
