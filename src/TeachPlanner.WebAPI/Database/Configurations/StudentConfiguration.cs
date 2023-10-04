﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Domain.Students;

namespace TeachPlanner.Api.Database.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.ToTable("students");

        builder.HasMany(s => s.Reports)
            .WithOne()
            .IsRequired();

        builder.HasMany(s => s.Assessments)
            .WithOne()
            .IsRequired();
    }
}
