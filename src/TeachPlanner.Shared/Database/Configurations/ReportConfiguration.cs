using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Database.Converters;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.Reports;
using TeachPlanner.Shared.Domain.Students;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Database.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report> {
    public void Configure(EntityTypeBuilder<Report> builder) {
        builder.ToTable("reports");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.ReportIdConverter());

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(r => r.TeacherId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<CurriculumSubject>()
            .WithMany()
            .HasForeignKey(r => r.SubjectId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Student>()
            .WithMany(s => s.Reports)
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(r => r.YearLevel)
            .HasConversion<string>()
            .HasMaxLength(15);

        builder.OwnsMany(r => r.ReportComments, cb => {
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