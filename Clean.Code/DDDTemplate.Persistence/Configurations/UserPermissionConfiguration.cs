using DDDTemplate.Domain.UserPermissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTemplate.Persistence.Configurations;

public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder
            .ToTable(nameof(UserPermission))
            .HasKey(x => new
            {
                x.IdGrantedResource,
                x.IdUser
            });

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.IdUser)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.GrantedResource)
            .WithMany()
            .HasForeignKey(x => x.IdGrantedResource)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Resource)
            .WithMany()
            .HasForeignKey(x => x.IdResource)
            .OnDelete(DeleteBehavior.Cascade);
    }
}