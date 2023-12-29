using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Blazor.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<UserId>, UserId>(options) {

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity => {
            entity.Property(e => e.Id).HasConversion(new UserId.StronglyTypedIdEfValueConverter());
            entity.Property(e => e.TeacherId).HasConversion(new TeacherId.StronglyTypedIdEfValueConverter());
        });

        builder.Entity<IdentityRole<UserId>>(entity => {
            entity.Property(e => e.Id).HasConversion(new UserId.IdToStringConverter());
        }); 
    }
}
