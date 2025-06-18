using AutoMapper;
using DrakarVpn.Core.AbstractsRepositories.Tariffs;
using DrakarVpn.Core.AbstractsServices.Tariffs;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Tariffs;

namespace DrakarVpn.Core.Services.Tariffs;

public class TariffService : ITariffService
{
    private readonly ITariffRepository tariffRepository;
    private readonly IMapper mapper;

    public TariffService(ITariffRepository tariffRepository, IMapper mapper)
    {
        this.tariffRepository = tariffRepository;
        this.mapper = mapper;
    }

    public async Task<List<TariffDto>> GetAllTariffsAsync()
    {
        var tariffs = await tariffRepository.GetAllTariffsAsync();
        return mapper.Map<List<TariffDto>>(tariffs);
    }

    public async Task<TariffDto?> GetTariffByIdAsync(Guid tariffId)
    {
        var tariff = await tariffRepository.GetTariffByIdAsync(tariffId);
        if (tariff == null) return null;

        return mapper.Map<TariffDto>(tariff);
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
            MaxDevices = dto.MaxDevices
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
        existing.MaxDevices = dto.MaxDevices;

        await tariffRepository.UpdateTariffAsync(existing);
    }

    public async Task<bool> DeleteTariffAsync(Guid tariffId)
    {
        var existing = await tariffRepository.GetTariffByIdAsync(tariffId);
        if (existing == null) return false;

        await tariffRepository.DeleteTariffAsync(existing);
        return true;
    }

    public async Task<List<TariffDto>> FilterTariffsAsync(TariffFilterDto filter)
    {
        var tariffs = await tariffRepository.FilterTariffsAsync(filter);
        return mapper.Map<List<TariffDto>>(tariffs);
    }
}
