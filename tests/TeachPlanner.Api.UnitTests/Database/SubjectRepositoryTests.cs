using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.Repositories;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Database;
public class SubjectRepositoryTests
{
    private readonly List<Subject> _subjects = SubjectHelpers.CreateSubjects();
    private readonly List<Subject> _curriculumSubjects = SubjectHelpers.CreateCurriculumSubjects();

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
            databaseContext.Subjects.AddRange(_subjects);
            databaseContext.Subjects.AddRange(_curriculumSubjects);
        }

        // if there are any change tracking issues, uncomment this
        //databaseContext.TermPlanners.AsNoTracking();

        await databaseContext.SaveChangesAsync();

        return databaseContext;
    }

    [Fact]
    public async void GetSubjectsById_ShouldReturnListOfSubjects()
    {
        // Arrange
        var context = await GetDbContext();
        var subjectRepository = new SubjectRepository(context);

        // Act
        var subjects = await subjectRepository.GetSubjectsById(_subjects.Select(s => s.Id).ToList(), false, default);

        // Assert
        subjects.Should().BeOfType<List<Subject>>();
        subjects.Should().HaveCount(_subjects.Count);
        subjects.Should().BeEquivalentTo(_subjects);
    }

    [Fact]
    public async void GetCurriculumSubjects_ShouldReturnAllCurriculumSubjects()
    {
        // Arrange
        var context = await GetDbContext();
        var subjectRepository = new SubjectRepository(context);

        // Act
        var subjects = await subjectRepository.GetCurriculumSubjects(false, default);

        // Assert
        subjects.Should().BeEquivalentTo(_curriculumSubjects);
    }
}
