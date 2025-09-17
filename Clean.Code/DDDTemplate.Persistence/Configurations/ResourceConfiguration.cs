using DDDTemplate.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTemplate.Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder
            .ToTable(nameof(Resource))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.ComplexProperty(x => x.Code, buildAction =>
        {
            buildAction.Property(x => x.Value)
                .HasColumnName("Code")
                .HasMaxLength(ResourceCode.MaxLength)
                .IsRequired();
        });
    }
}