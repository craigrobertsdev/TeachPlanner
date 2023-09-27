using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.Calendar;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Domain.Reports;
using TeachPlanner.Domain.Resources;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;
using TeachPlanner.Domain.TermPlanners;
using TeachPlanner.Domain.WeekPlanners;
using TeachPlanner.Domain.YearDataRecord;

namespace TeachPlanner.Infrastructure.Persistence.DbContexts;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<Resource> Resources { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<SummativeAssessment> SummativeAssessments { get; set; } = null!;
    public DbSet<FormativeAssessment> FormativeAssessments { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;
    public DbSet<LessonPlan> LessonPlans { get; set; } = null!;
    public DbSet<WeekPlanner> WeekPlanners { get; set; } = null!;
    public DbSet<TermPlanner> TermPlanners { get; set; } = null!;
    public DbSet<Calendar> Calendar { get; set; } = null!;
    public DbSet<YearData> YearData { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
