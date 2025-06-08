using DrakarVpn.Domain.ModelDto.Tariffs;

namespace DrakarVpn.Core.AbstractsServices.Tariffs;

public interface ITariffService
{
    Task<List<TariffDto>> GetAllTariffsAsync();
    Task<TariffDto?> GetTariffByIdAsync(Guid tariffId);
    Task CreateTariffAsync(TariffCreateUpdateDto dto);
    Task UpdateTariffAsync(Guid tariffId, TariffCreateUpdateDto dto);
    Task<bool> DeleteTariffAsync(Guid tariffId);
    Task<List<TariffDto>> FilterTariffsAsync(TariffFilterDto filter);

}
