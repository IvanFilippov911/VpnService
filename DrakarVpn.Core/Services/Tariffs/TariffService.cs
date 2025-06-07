using DrakarVpn.Core.AbstractsRepositories.Tariffs;
using DrakarVpn.Core.AbstractsServices.Tariffs;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Tariffs;

namespace DrakarVpn.Core.Services.Tariffs;

public class TariffService : ITariffService
{
    private readonly ITariffRepository tariffRepository;

    public TariffService(ITariffRepository tariffRepository)
    {
        this.tariffRepository = tariffRepository;
    }

    public async Task<List<TariffDto>> GetAllTariffsAsync()
    {
        var tariffs = await tariffRepository.GetAllTariffsAsync();

        return tariffs.Select(t => new TariffDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            Price = t.Price,
            DurationInDays = t.DurationInDays,
            Limitations = t.Limitations
        }).ToList();
    }

    public async Task<TariffDto?> GetTariffByIdAsync(Guid tariffId)
    {
        var tariff = await tariffRepository.GetTariffByIdAsync(tariffId);
        if (tariff == null) return null;

        return new TariffDto
        {
            Id = tariff.Id,
            Name = tariff.Name,
            Description = tariff.Description,
            Price = tariff.Price,
            DurationInDays = tariff.DurationInDays,
            Limitations = tariff.Limitations
        };
    }

    public async Task CreateTariffAsync(TariffCreateUpdateDto dto)
    {
        var tariff = new Tariff
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            DurationInDays = dto.DurationInDays,
            Limitations = dto.Limitations
        };

        await tariffRepository.AddTariffAsync(tariff);
    }

    public async Task UpdateTariffAsync(Guid tariffId, TariffCreateUpdateDto dto)
    {
        var existing = await tariffRepository.GetTariffByIdAsync(tariffId);
        if (existing == null) return;

        existing.Name = dto.Name;
        existing.Description = dto.Description;
        existing.Price = dto.Price;
        existing.DurationInDays = dto.DurationInDays;
        existing.Limitations = dto.Limitations;

        await tariffRepository.UpdateTariffAsync(existing);
    }

    public async Task<bool> DeleteTariffAsync(Guid tariffId)
    {
        var existing = await tariffRepository.GetTariffByIdAsync(tariffId);
        if (existing == null) return false;

        await tariffRepository.DeleteTariffAsync(existing);
        return true;
    }
}
