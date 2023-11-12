using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.Calendar;
using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Reports;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Domain.WeekPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database;

public class ApplicationDbContext : DbContext
{
    private readonly IPublisher _publisher = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<CurriculumSubject> CurriculumSubjects { get; set; } = null!;
    public DbSet<Resource> Resources { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Assessment> Assessments { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;
    public DbSet<LessonPlan> LessonPlans { get; set; } = null!;
    public DbSet<WeekPlanner> WeekPlanners { get; set; } = null!;
    public DbSet<DayPlanTemplate> DayPlanTemplates { get; set; } = null!;
    public DbSet<TermPlanner> TermPlanners { get; set; } = null!;
    public DbSet<Calendar> Calendar { get; set; } = null!;
    public DbSet<YearData> YearData { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entitiesWithDomainEvents = ChangeTracker.Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        var domainEvents = entitiesWithDomainEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entitiesWithDomainEvents.ForEach(e => e.ClearDomainEvents());

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents) await _publisher.Publish(domainEvent, cancellationToken);
        return result;
    }
}