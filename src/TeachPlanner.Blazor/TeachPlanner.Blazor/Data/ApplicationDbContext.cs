using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options) {

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity => {
            entity.Property(e => e.TeacherId).HasConversion(new TeacherId.StronglyTypedIdEfValueConverter());
        });
    }
}
