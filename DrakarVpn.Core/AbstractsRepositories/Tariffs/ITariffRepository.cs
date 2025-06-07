using DrakarVpn.Domain.Entities;


namespace DrakarVpn.Core.AbstractsRepositories.Tariffs;

public interface ITariffRepository
{
    Task<List<Tariff>> GetAllTariffsAsync();
    Task<Tariff?> GetTariffByIdAsync(Guid tariffId);
    Task AddTariffAsync(Tariff tariff);
    Task UpdateTariffAsync(Tariff tariff);
    Task DeleteTariffAsync(Tariff tariff);
}