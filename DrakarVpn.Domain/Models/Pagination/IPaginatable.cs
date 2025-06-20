namespace DrakarVpn.Domain.Models.Pagination;

public interface IPaginatable
{
    int Offset { get; set; }
    int Limit { get; set; }
}