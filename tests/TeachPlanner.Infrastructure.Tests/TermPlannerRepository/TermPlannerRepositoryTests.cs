using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.TermPlanners;
using TeachPlanner.Infrastructure.Persistence.DbContexts;
using TeachPlanner.Infrastructure.Persistence.Repositories;

namespace TeachPlanner.Infrastructure.Tests.TermPlannerRepositoryTests;
public class TermPlannerRepositoryTests
{
    private readonly IPublisher _publisher;

    public TermPlannerRepositoryTests(IPublisher publisher)
    {
        _publisher = publisher;
    }
    private async Task<ApplicationDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TeachPlanner")
            .Options;

        var databaseContext = new ApplicationDbContext(options, _publisher);
        databaseContext.Database.EnsureCreated();

        if (await databaseContext.TermPlanners.CountAsync() == 0)
        {
            var termPlanner = TermPlanner.Create(Guid.NewGuid(), 2023, new List<YearLevelValue> { YearLevelValue.Foundation, YearLevelValue.Year1 });
            var termPlan = TermPlan.Create(termPlanner, 1, new List<Subject>());

            termPlanner.AddTermPlan(termPlan);

            var subject = Subject.Create("English", new List<YearLevel>());
            var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
            var strand = Strand.Create(yearLevel, "Grammar", new List<Substrand>(), null);
            var substrand = Substrand.Create("Grammar constructs", new List<ContentDescription>(), strand);
            var contentDescription = ContentDescription.Create("Description", "ENG001", new List<Elaboration>(), substrand: substrand);

            subject.AddYearLevel(yearLevel);
            yearLevel.AddStrand(strand);
            strand.AddSubstrand(substrand);
            substrand.AddContentDescription(contentDescription);

            termPlan.AddContentDescription(contentDescription);

            databaseContext.TermPlanners.Add(termPlanner);

            // if there are any change tracking issues, uncomment this
            //databaseContext.TermPlanners.AsNoTracking();

            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
    }


    [Fact]
    public async void TermPlannerRepository_Add_AddsTermPlanner()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var termPlannerRepository = new TermPlannerRepository(dbContext);
        var termPlanner = TermPlanner.Create(Guid.NewGuid(), 2023, new List<YearLevelValue> { YearLevelValue.Foundation, YearLevelValue.Year1 });

        // Act
        termPlannerRepository.Add(termPlanner);
        await dbContext.SaveChangesAsync();

        // Assert
        dbContext.TermPlanners.Should().Contain(termPlanner);
    }

    [Fact]
    public async void TermPlannerRepository_Get_ReturnsTermPlanner()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var termPlannerRepository = new TermPlannerRepository(dbContext);
        var termPlanner = TermPlanner.Create(Guid.NewGuid(), 2023, new List<YearLevelValue> { YearLevelValue.Foundation, YearLevelValue.Year1 });

        // Act
        termPlannerRepository.Add(termPlanner);
        await dbContext.SaveChangesAsync();
        var result = await termPlannerRepository.GetById(termPlanner.Id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(termPlanner);
        result!.Id.Should().Be(termPlanner.Id);
    }

    [Fact]
    public async void TermPlannerRepository_Delete_DeletesTermPlanner()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var termPlannerRepository = new TermPlannerRepository(dbContext);
        var termPlanner = TermPlanner.Create(Guid.NewGuid(), 2023, new List<YearLevelValue> { YearLevelValue.Foundation, YearLevelValue.Year1 });

        // Act
        termPlannerRepository.Add(termPlanner);
        await dbContext.SaveChangesAsync();
        await termPlannerRepository.Delete(termPlanner.Id, new CancellationToken());
        await dbContext.SaveChangesAsync();

        // Assert
        dbContext.TermPlanners.Should().NotContain(termPlanner);
    }
}
