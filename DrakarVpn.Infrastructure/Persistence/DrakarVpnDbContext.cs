using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DrakarVpn.Domain.Entities;

namespace DrakarVpn.Infrastructure.Persistence;

public class DrakarVpnDbContext : IdentityDbContext
{
    public DrakarVpnDbContext(DbContextOptions<DrakarVpnDbContext> options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Peer> Peers { get; set; } = null!;
    public DbSet<UserVpnDevice> UserVpnDevices { get; set; }



}
