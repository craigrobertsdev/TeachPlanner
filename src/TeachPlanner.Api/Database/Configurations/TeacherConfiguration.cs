using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Database.Converters;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Api.Database.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
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

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Teacher>(t => t.UserId)
            .IsRequired();

        builder.OwnsMany(t => t.YearDataHistory, ydb =>
        {
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