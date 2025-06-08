using DrakarVpn.Core.AbstractsRepositories.Tariffs;
using DrakarVpn.Domain.Entities;
using DrakarVpn.Domain.ModelDto.Tariffs;
using DrakarVpn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DrakarVpn.Infrastructure.Repositories;

public class TariffRepository : ITariffRepository
{
    private readonly DrakarVpnDbContext dbContext;

    public TariffRepository(DrakarVpnDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Tariff>> GetAllTariffsAsync()
    {
        return await dbContext.Tariffs.AsNoTracking().ToListAsync();
    }

    public async Task<Tariff?> GetTariffByIdAsync(Guid tariffId)
    {
        return await dbContext.Tariffs.FirstOrDefaultAsync(t => t.Id == tariffId);
    }

    public async Task AddTariffAsync(Tariff tariff)
    {
        await dbContext.Tariffs.AddAsync(tariff);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateTariffAsync(Tariff tariff)
    {
        await dbContext.Tariffs
            .Where(t => t.Id == tariff.Id)
            .ExecuteUpdateAsync(i => i
                .SetProperty(t => t.Name, t => tariff.Name)
                .SetProperty(t => t.Description, t => tariff.Description)
                .SetProperty(t => t.Price, t => tariff.Price)
                .SetProperty(t => t.DurationInDays, t => tariff.DurationInDays)
                .SetProperty(t => t.Limitations, t => tariff.Limitations)
            );
    }

    public async Task DeleteTariffAsync(Tariff tariff)
    {
        dbContext.Tariffs.Remove(tariff);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Tariff>> FilterTariffsAsync(TariffFilterDto filter)
    {
        var query = dbContext.Tariffs.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(t => t.Name.Contains(filter.Name));

        if (filter.MinPrice.HasValue)
            query = query.Where(t => t.Price >= filter.MinPrice);

        if (filter.MaxPrice.HasValue)
            query = query.Where(t => t.Price <= filter.MaxPrice);

        if (filter.MinDurationDays.HasValue)
            query = query.Where(t => t.DurationInDays >= filter.MinDurationDays);

        if (filter.MaxDurationDays.HasValue)
            query = query.Where(t => t.DurationInDays <= filter.MaxDurationDays);

        return await query.AsNoTracking().ToListAsync();
    }


}