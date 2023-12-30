using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Database.Converters;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Shared.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.UserIdConverter());

        builder.Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(u => u.Password)
            .HasMaxLength(255)
            .IsRequired();
    }
}