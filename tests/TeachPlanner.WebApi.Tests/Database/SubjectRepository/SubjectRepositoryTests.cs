using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.WebApi.Tests.Database.SubjectRepository;
public class SubjectRepositoryTests
{
    private async Task<ApplicationDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TeachPlanner")
            .Options;

        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureDeleted();
        databaseContext.Database.EnsureCreated();

        if (!await databaseContext.TermPlanners.AnyAsync())
        {
            for (int i = 0; i < 10; i++)
            {
                var subject = Subject.Create("English" + i, new List<YearLevel>());
                var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description" + i, "Achievement Standard", YearLevelValue.Foundation, null);
                var strand = Strand.Create(yearLevel, "Grammar" + i, new List<Substrand>(), null);
                var substrand = Substrand.Create("Grammar constructs" + i, new List<ContentDescription>(), strand);
                var contentDescription = ContentDescription.Create("Description", "ENG001" + i, new List<Elaboration>(), substrand: substrand);

                subject.AddYearLevel(yearLevel);
                yearLevel.AddStrand(strand);
                strand.AddSubstrand(substrand);
                substrand.AddContentDescription(contentDescription);

                databaseContext.Subjects.Add(subject);
            }

            // if there are any change tracking issues, uncomment this
            //databaseContext.TermPlanners.AsNoTracking();

            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
    }

    [Fact]
    public async void GetSubjects_ShouldReturnListOfSubjects()
    {
        // Arrange
        var context = await GetDbContext();
        var subjectIds = context.Subjects.Select(s => s.Id).ToList();

        // Act
        var subjects = await context.GetSubjectsById(subjectIds!, false, new CancellationToken());

        // Assert
        subjects.Should().BeOfType<List<Subject>>();
        subjects.Should().HaveCount(subjectIds.Count);
    }
}
