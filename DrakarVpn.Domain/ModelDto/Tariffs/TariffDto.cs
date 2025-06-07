namespace DrakarVpn.Domain.ModelDto.Tariffs;

public class TariffDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DurationInDays { get; set; } 
    public string Limitations { get; set; } 
}
