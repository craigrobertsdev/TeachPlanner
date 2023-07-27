using Domain.ReportAggregate;
using Domain.ReportAggregate.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        ConfigureReportTable(builder);
        ConfigureReportCommentTable(builder);
    }

    private void ConfigureReportCommentTable(EntityTypeBuilder<Report> builder)
    {
        builder.OwnsMany(r => r.ReportComments, cb =>
        {
            cb.ToTable("ReportComments");

            cb.WithOwner().HasForeignKey("ReportId");

            cb.HasKey("Id");

            cb.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(c => c.Value, value => new ReportCommentId(value));

            cb.Property(c => c.SubjectId)
                .ValueGeneratedNever()
                .HasConversion(s => s.Value, value => new SubjectId(value));

            cb.Property(c => c.Grade)
                .HasConversion<string>();
        });

        builder.Metadata.FindNavigation(nameof(Report.ReportComments))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    public void ConfigureReportTable(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");

        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasConversion(r => r.Value, value => new ReportId(value));

        builder.Property(r => r.TeacherId)
            .HasConversion(t => t.Value, value => new TeacherId(value));

        builder.Property(r => r.StudentId)
            .HasConversion(s => s.Value, value => new StudentId(value));

        builder.Property(r => r.SubjectId)
            .HasConversion(s => s.Value, value => new SubjectId(value));

        builder.Property(r => r.YearLevel)
            .HasConversion<string>();
    }
}
