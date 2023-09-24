using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Infrastructure.Persistence.DbContexts;
using TeachPlanner.Infrastructure.Persistence.Repositories;

namespace TeachPlanner.Infrastructure.Tests.SubjectRepositoryTests;
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

        if (!(await databaseContext.TermPlanners.AnyAsync()))
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
    public async void SubjectRepository_GetSubjects_ShouldReturnListOfSubjects()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var subjectRepository = new SubjectRepository(dbContext);
        var subjectIds = dbContext.Subjects.Select(s => s.Id).ToList();

        // Act
        var subjects = await subjectRepository.GetSubjectsById(subjectIds, new CancellationToken());

        // Assert
        subjects.Should().BeOfType<List<Subject>>();
        subjects.Should().HaveCount(subjectIds.Count);
    }
}
