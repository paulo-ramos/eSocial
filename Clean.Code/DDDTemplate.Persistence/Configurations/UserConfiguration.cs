using DDDTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTemplate.Persistence.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable(nameof(User))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Username)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .ComplexProperty(x => x.Password, builderAction =>
            {
                builderAction
                    .Property(x => x.Value)
                    .HasColumnName(nameof(Password))
                    .HasMaxLength(Password.MaxDbLength)
                    .IsRequired();
            });
        
        
    }
}