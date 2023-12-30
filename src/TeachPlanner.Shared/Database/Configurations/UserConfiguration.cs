using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Shared.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser> {
    public void Configure(EntityTypeBuilder<ApplicationUser> builder) {
    }
}