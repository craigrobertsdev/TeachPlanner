using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Reports;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teacher;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

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

        builder.Property(r => r.YearLevel)
            .HasConversion<string>();

        builder.OwnsMany(r => r.ReportComments, cb =>
        {
            cb.ToTable("report_comments");

            cb.WithOwner().HasForeignKey("ReportId");

            cb.Property<Guid>("Id");

            cb.HasKey("Id");

            cb.Property(c => c.Grade)
                .HasConversion<string>();
        });

        /*        builder.Navigation(r => r.ReportComments).Metadata.SetField("_reportComments");
                builder.Navigation(lp => lp.ReportComments).UsePropertyAccessMode(PropertyAccessMode.Field);
        */
    }
}
