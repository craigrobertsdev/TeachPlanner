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
            cb.ToTable("report_comments");

            cb.WithOwner().HasForeignKey("ReportId");

            cb.HasKey("Id");

            cb.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(c => c.Value, value => ReportCommentId.Create(value));

            cb.Property(c => c.SubjectId)
                .ValueGeneratedNever()
                .HasConversion(s => s.Value, value => SubjectId.Create(value));

            cb.Property(c => c.Grade)
                .HasConversion<string>();
        });

        builder.Metadata.FindNavigation(nameof(Report.ReportComments))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    public void ConfigureReportTable(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("reports");

        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasConversion(r => r.Value, value => ReportId.Create(value));

        builder.Property(r => r.TeacherId)
            .HasConversion(t => t.Value, value => TeacherIdForReference.Create(value));

        builder.Property(r => r.StudentId)
            .HasConversion(s => s.Value, value => StudentIdForReference.Create(value));

        builder.Property(r => r.SubjectId)
            .HasConversion(s => s.Value, value => SubjectIdForReference.Create(value));

        builder.Property(r => r.YearLevel)
            .HasConversion<string>();
    }
}
