using Domain.TeacherAggregate;
using Domain.TeacherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        ConfigureTeacherTable(builder);
        ConfigureTeacherSubjectIdTable(builder);
        ConfigureTeacherStudentIdTable(builder);
        ConfigureTeacherAssessmentIdTable(builder);
        ConfigureTeacherResourceIdTable(builder);
        ConfigureTeacherReportIdTable(builder);
    }

    private void ConfigureTeacherReportIdTable(EntityTypeBuilder<Teacher> builder)
    {
        builder.OwnsMany(t => t.ReportIds, rib =>
        {
            rib.ToTable("teacher_report_ids");

            rib.WithOwner().HasForeignKey("TeacherId");

            rib.HasKey("Id");

            rib.Property(r => r.Value)
                .HasColumnName("TeacherReportId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Teacher.ReportIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTeacherResourceIdTable(EntityTypeBuilder<Teacher> builder)
    {
        builder.OwnsMany(t => t.ResourceIds, rib =>
        {
            rib.ToTable("teacher_resource_ids");

            rib.WithOwner().HasForeignKey("TeacherId");

            rib.HasKey("Id");

            rib.Property(r => r.Value)
                .HasColumnName("TeacherResourceId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Teacher.ResourceIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTeacherAssessmentIdTable(EntityTypeBuilder<Teacher> builder)
    {
        builder.OwnsMany(t => t.AssessmentIds, aib =>
        {
            aib.ToTable("teacher_assessment_ids");

            aib.WithOwner().HasForeignKey("TeacherId");

            aib.HasKey("Id");

            aib.Property(a => a.Value)
                .HasColumnName("TeacherAssessmentId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Teacher.AssessmentIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTeacherStudentIdTable(EntityTypeBuilder<Teacher> builder)
    {
        builder.OwnsMany(t => t.StudentIds, sib =>
        {
            sib.ToTable("teacher_student_ids");

            sib.WithOwner().HasForeignKey("TeacherId");

            sib.HasKey("Id");

            sib.Property(s => s.Value)
                .HasColumnName("TeacherStudentId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Teacher.StudentIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTeacherSubjectIdTable(EntityTypeBuilder<Teacher> builder)
    {
        builder.OwnsMany(t => t.SubjectIds, sib =>
        {
            sib.ToTable("teacher_subject_ids");

            sib.WithOwner().HasForeignKey("TeacherId");

            sib.HasKey("Id");

            sib.Property(s => s.Value)
                .HasColumnName("TeacherSubjectId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Teacher.SubjectIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTeacherTable(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("teachers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TeacherId.Create(value));

        builder.OwnsOne(t => t.UserId, uib =>
        {
            uib.Property(u => u.Value)
                .HasColumnName("UserId")
                .ValueGeneratedNever();
        });
    }
}
