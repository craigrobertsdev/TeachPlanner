using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Domain.Reports;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Database.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("reports");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("Id");

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(r => r.TeacherId)
            .IsRequired();

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(r => r.SubjectId)
            .IsRequired();

        builder.HasOne<Student>()
            .WithMany(s => s.Reports)
            .HasForeignKey(r => r.StudentId)
            .IsRequired();

        builder.Property(r => r.YearLevel)
            .HasConversion<string>()
            .HasMaxLength(15);

        builder.OwnsMany(r => r.ReportComments, cb =>
        {
            cb.ToTable("report_comments");

            cb.WithOwner().HasForeignKey("ReportId");

            cb.Property<Guid>("Id");

            cb.HasKey("Id");

            cb.Property(c => c.Comments)
                .HasMaxLength(500);

            cb.Property(c => c.Grade)
                .HasConversion<string>()
                .HasMaxLength(10);
        });
    }
}
