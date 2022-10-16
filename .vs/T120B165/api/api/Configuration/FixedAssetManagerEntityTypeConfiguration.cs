using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using api.Domain;

namespace api.Configuration;

public class FixedAssetManagerEntityTypeConfiguration : IEntityTypeConfiguration<FixedAssetManager>
{
    public void Configure(EntityTypeBuilder<FixedAssetManager> builder)
    {
        builder.ToTable("FixedAssetsManagers");
        builder.HasKey(x => new { x.Username, x.FACategory });
    }
}