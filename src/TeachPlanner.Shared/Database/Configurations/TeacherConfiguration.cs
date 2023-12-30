using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Database.Converters;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Shared.Database.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher> {
    public void Configure(EntityTypeBuilder<Teacher> builder) {
        builder.ToTable("teachers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.TeacherIdConverter());

        builder.Property(t => t.FirstName).HasMaxLength(50);

        builder.Property(t => t.LastName).HasMaxLength(50);

        builder.HasMany(t => t.Assessments)
            .WithOne()
            .IsRequired();

        builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Teacher>(t => t.UserId)
            .IsRequired();

        builder.HasMany(t => t.SubjectsTaught)
            .WithMany();

        builder.OwnsMany(t => t.YearDataHistory, ydb => {
            ydb.ToTable("year_data_entries");
            ydb.Property<Guid>("Id");
            ydb.WithOwner().HasForeignKey("TeacherId");
            ydb.Property(yd => yd.CalendarYear).HasColumnName("CalendarYear");
            ydb.Property(yd => yd.YearDataId)
                .HasColumnName("YearDataId")
                .HasConversion(new StronglyTypedIdConverter.YearDataIdConverter());
        });
    }
}