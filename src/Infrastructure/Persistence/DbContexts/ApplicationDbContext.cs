using Domain.Assessments;
using Domain.LessonPlanAggregate;
using Domain.ReportAggregate;
using Domain.ResourceAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Domain.TermPlannerAggregate;
using Domain.TimeTableAggregate;
using Domain.UserAggregate;
using Domain.YearPlannerAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Resource> Resources { get; set; } = null!;
    /*    public DbSet<User> Users { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<SummativeAssessment> SummativeAssessments { get; set; } = null!;
        public DbSet<FormativeAssessment> FormativeAssessments { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        //public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<LessonPlan> LessonPlans { get; set; } = null!;
        public DbSet<WeekPlanner> WeekPlanners { get; set; } = null!;
        public DbSet<TermPlanner> TermPlanners { get; set; } = null!;
        public DbSet<YearPlanner> YearPlanners { get; set; } = null!;
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
