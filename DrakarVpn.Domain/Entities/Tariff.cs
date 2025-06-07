

namespace DrakarVpn.Domain.Entities;

public class Tariff
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
    public string Limitations { get; set; }
}
