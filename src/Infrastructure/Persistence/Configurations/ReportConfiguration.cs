using Domain.ReportAggregate;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("reports");

        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasConversion(r => r.Value, value => new ReportId(value));

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(r => r.TeacherId)
            .IsRequired();

        builder.OwnsOne(r => r.StudentId, sib =>
        {
            sib.ToTable("student_report");

            sib.WithOwner().HasForeignKey("ReportId");

            sib.Property(s => s.Value)
                .HasColumnName("StudentId");
        });

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(r => r.SubjectId)
            .IsRequired();

        builder.Property(r => r.YearLevel)
            .HasConversion<string>();

        builder.OwnsMany(r => r.ReportComments, cb =>
        {
            cb.ToTable("report_comments");

            cb.WithOwner().HasForeignKey("ReportId");

            cb.HasKey("Id");

            cb.HasOne<Subject>()
                .WithMany()
                .HasForeignKey(c => c.SubjectId)
                .IsRequired();

            cb.Property(c => c.Grade)
                .HasConversion<string>();
        });

        builder.Metadata.FindNavigation(nameof(Report.ReportComments))!.SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}
