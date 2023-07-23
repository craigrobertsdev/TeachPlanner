using Domain.LessonPlanAggregate;
using Domain.LessonPlanAggregate.ValueObjects;
using Domain.Resource;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class LessonPlanConfiguration : IEntityTypeConfiguration<LessonPlan>
{
    public void Configure(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Id)
            .HasConversion(lp => lp.Value, lp => new LessonPlanId(lp));

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(t => t.Id)
            .IsRequired();

        builder.HasMany<Resource>()
            .WithMany();

        builder.OwnsMany(lp => lp.Comments)
            .WithOwner()
    }
}
