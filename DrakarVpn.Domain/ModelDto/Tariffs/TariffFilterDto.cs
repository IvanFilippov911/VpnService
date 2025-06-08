

namespace DrakarVpn.Domain.ModelDto.Tariffs;

public class TariffFilterDto
{
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinDurationDays { get; set; }
    public int? MaxDurationDays { get; set; }
    
}
