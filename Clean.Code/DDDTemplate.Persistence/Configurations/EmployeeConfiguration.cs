using DDDTemplate.Domain.Employees;
using DDDTemplate.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTemplate.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .ToTable(nameof(Employee))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Fullname)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .ComplexProperty(x => x.EmailAddress, builderAction =>
            {
                builderAction
                    .Property(x => x.Value)
                    .HasColumnName(nameof(EmailAddress))
                    .HasMaxLength(EmailAddress.MaxLength);
            });

        builder
            .ComplexProperty(x => x.MobileNumber, builderAction =>
            {
                builderAction
                    .Property(x => x.Value)
                    .HasColumnName(nameof(MobileNumber))
                    .HasMaxLength(MobileNumber.MaxLength)
                    .IsRequired();
            });

        builder
            .Property(x => x.Address)
            .HasMaxLength(250);

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Employee)
            .HasForeignKey<Employee>(x => x.IdUser)
            .OnDelete(DeleteBehavior.Cascade);
    }
}