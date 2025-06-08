using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Tariffs;


namespace DrakarVpn.Core.AbstractsRepositories.Tariffs;

public interface ITariffRepository
{
    Task<List<Tariff>> GetAllTariffsAsync();
    Task<Tariff?> GetTariffByIdAsync(Guid tariffId);
    Task AddTariffAsync(Tariff tariff);
    Task UpdateTariffAsync(Tariff tariff);
    Task DeleteTariffAsync(Tariff tariff);
    Task<List<Tariff>> FilterTariffsAsync(TariffFilterDto filter);

}