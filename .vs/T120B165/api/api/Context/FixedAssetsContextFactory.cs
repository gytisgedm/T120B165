using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace api.Context;

public class FixedAssetsContextFactory : IDesignTimeDbContextFactory<FixedAssetsContext>
{
    public FixedAssetsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FixedAssetsContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FixedAssets;Trusted_Connection=True");
        return new FixedAssetsContext(optionsBuilder.Options);
    }
}