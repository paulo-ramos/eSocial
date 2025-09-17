using DDDTemplate.Domain.GrantedResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTemplate.Persistence.Configurations;

public class GrantedResourceConfiguration : IEntityTypeConfiguration<GrantedResource>
{
    public void Configure(EntityTypeBuilder<GrantedResource> builder)
    {
        builder
            .ToTable(nameof(GrantedResource))
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.Resource)
            .WithMany(x => x.GrantedResources)
            .HasForeignKey(x => x.IdResource)
            .OnDelete(DeleteBehavior.Cascade);
    }
}