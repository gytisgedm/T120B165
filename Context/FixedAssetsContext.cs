using System.Reflection;
using api.Domain;
using Microsoft.EntityFrameworkCore;

namespace api.Context;

public class FixedAssetsContext : DbContext
{
    public FixedAssetsContext(DbContextOptions<FixedAssetsContext> o) : base(o) { }

    public DbSet<FixedAsset> FixedAssets { get; set; }
    public DbSet<FixedAssetManager> FixedAssetManagers { get; set; }
    public DbSet<FixedAssetEvent> FixedAssetEvents { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}