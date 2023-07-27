using Domain.StudentAggregate;
using Domain.StudentAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        ConfigureStudentsTable(builder);
        ConfigureStudentReportIdsTable(builder);
        ConfigureStudentSubjectIdsTable(builder);
        ConfigureStudentAssessmentIdsTable(builder);
    }

    private void ConfigureStudentReportIdsTable(EntityTypeBuilder<Student> builder)
    {
        builder.OwnsMany(s => s.ReportIds, rib =>
        {
            rib.ToTable("StudentReportIds");

            rib.WithOwner().HasForeignKey("StudentId");

            rib.HasKey("Id");

            rib.Property(r => r.Value)
                .HasColumnName("ReportId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Student.ReportIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureStudentSubjectIdsTable(EntityTypeBuilder<Student> builder)
    {
        builder.OwnsMany(s => s.SubjectIds, sib =>
        {
            sib.ToTable("StudentSubjectIds");

            sib.WithOwner().HasForeignKey("StudentId");

            sib.HasKey("Id");

            sib.Property(s => s.Value)
                .HasColumnName("SubjectId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Student.SubjectIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureStudentAssessmentIdsTable(EntityTypeBuilder<Student> builder)
    {
        builder.OwnsMany(s => s.AssessmentIds, aib =>
        {
            aib.ToTable("StudentAssessmentIds");

            aib.WithOwner().HasForeignKey("StudentId");

            aib.HasKey("Id");

            aib.Property(a => a.Value)
                .HasColumnName("AssessmentId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Student.AssessmentIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    public void ConfigureStudentsTable(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(s => s.Value, value => new StudentId(value))
            .ValueGeneratedNever();
    }
}
