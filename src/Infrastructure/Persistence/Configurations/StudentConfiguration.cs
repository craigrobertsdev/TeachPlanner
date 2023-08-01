using Domain.StudentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(s => s.Value, value => new StudentId(value))
            .ValueGeneratedNever();

        builder.OwnsMany(s => s.ReportIds);

        builder.OwnsMany(s => s.SummativeAssessmentIds, sib =>
        {
            sib.WithOwner().HasForeignKey("StudentId");

            sib.ToTable("student_summative_assessment");
        });

        builder.OwnsMany(s => s.FormativeAssessmentIds, fib =>
        {
            fib.WithOwner().HasForeignKey("StudentId");

            fib.ToTable("student_formative_assessment");
        });

        builder.OwnsOne(s => s.TeacherId);

        builder.Navigation(s => s.ReportIds).Metadata.SetField("_reportIds");
        builder.Navigation(s => s.ReportIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
