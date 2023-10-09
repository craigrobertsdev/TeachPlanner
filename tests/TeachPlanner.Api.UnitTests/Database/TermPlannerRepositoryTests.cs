using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.UnitTests.Database;
public class TermPlannerRepositoryTests
{
    private readonly IPublisher _publisher;

    public TermPlannerRepositoryTests()
    {
        _publisher = A.Fake<IPublisher>();
    }
    private async Task<ApplicationDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TeachPlanner")
            .Options;

        var databaseContext = new ApplicationDbContext(options, _publisher);
        databaseContext.Database.EnsureCreated();

        if (await databaseContext.TermPlanners.AnyAsync())
        {
            var termPlanner = TermPlanner.Create(new YearDataId(Guid.NewGuid()), 2023, new List<YearLevelValue> { YearLevelValue.Foundation, YearLevelValue.Year1 });
            var termPlan = TermPlan.Create(termPlanner, 1, new List<Subject>());

            termPlanner.AddTermPlan(termPlan);

            var subject = Subject.Create("English", new List<YearLevel>());
            var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
            var strand = Strand.Create("Grammar", new List<ContentDescription>());
            var contentDescription = ContentDescription.Create("Description", "ENG001", new List<Elaboration>());

            subject.AddYearLevel(yearLevel);
            yearLevel.AddStrand(strand);
            strand.AddContentDescription(contentDescription);

            termPlan.AddSubject(subject);

            databaseContext.TermPlanners.Add(termPlanner);

            // if there are any change tracking issues, uncomment this
            //databaseContext.TermPlanners.AsNoTracking();

            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
    }

    [Fact]
    public async void TermPlannerRepository_Get_ReturnsTermPlanner()
    {
        // Arrange
        var context = await GetDbContext();
        var termPlanner = TermPlanner.Create(new YearDataId(Guid.NewGuid()), 2023, new List<YearLevelValue> { YearLevelValue.Foundation, YearLevelValue.Year1 });

        // Act
        context.Add(termPlanner);
        await context.SaveChangesAsync();
        var result = await context.GetTermPlannerById(termPlanner.Id, new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(termPlanner);
        result!.Id.Should().Be(termPlanner.Id);
    }
}
