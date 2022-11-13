using api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configuration;

public class FixedAssetEventEntityTypeConfiguration : IEntityTypeConfiguration<FixedAssetEvent>
{
    public void Configure(EntityTypeBuilder<FixedAssetEvent> builder)
    {
        builder.ToTable("FixedAssetEvents");
        builder.HasDiscriminator(e => e.Type)
            .HasValue<FixedAssetEvent>(FixedAssetEventType.Unassigned)
            .HasValue<AssignedToUserEvent>(FixedAssetEventType.AssignedToUserByManager)
            .HasValue<AcceptedByUserEvent>(FixedAssetEventType.AcceptedByUser)
            .HasValue<RejectedByUserEvent>(FixedAssetEventType.RejectedByUser)
            .HasValue<ReturnInitiatedByManagerEvent>(FixedAssetEventType.ReturnInitiatedByManager)
            .HasValue<StoredByManagerEvent>(FixedAssetEventType.StoredByManager);
    }
}