using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Domain.Assessments;
using TeachPlanner.Shared.Domain.Calendar;
using TeachPlanner.Shared.Domain.Common.Interfaces;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.LessonPlans;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Reports;
using TeachPlanner.Shared.Domain.Students;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.TermPlanners;
using TeachPlanner.Shared.Domain.Users;
using TeachPlanner.Shared.Domain.WeekPlanners;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Shared.Database;

public class ApplicationDbContext : DbContext {
    private readonly IPublisher _publisher = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
        : base(options) {
        _publisher = publisher;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
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
    public DbSet<TermDate> TermDates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) {
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