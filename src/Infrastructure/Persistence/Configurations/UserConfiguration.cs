using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(u => u.FirstName).HasMaxLength(50);

        builder.Property(u => u.LastName).HasMaxLength(50);

        builder.Property(u => u.Email).HasMaxLength(255);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
