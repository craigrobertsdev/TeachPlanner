using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.Repositories;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Database;
public class SubjectRepositoryTests {
    private readonly List<Subject> _subjects = SubjectHelpers.CreateSubjects();
    private readonly List<CurriculumSubject> _curriculumSubjects = SubjectHelpers.CreateCurriculumSubjects();

    private async Task<ApplicationDbContext> GetDbContext() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TeachPlanner")
            .Options;

        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureDeleted();
        databaseContext.Database.EnsureCreated();

        if (!await databaseContext.TermPlanners.AnyAsync()) {
            databaseContext.CurriculumSubjects.AddRange(_curriculumSubjects);
        }

        // if there are any change tracking issues, uncomment this
        //databaseContext.TermPlanners.AsNoTracking();

        await databaseContext.SaveChangesAsync();

        return databaseContext;
    }

    [Fact]
    public async void GetCurriculumSubjects_ShouldReturnAllCurriculumSubjects() {
        // Arrange
        var context = await GetDbContext();
        var subjectRepository = new SubjectRepository(context);

        // Act
        var subjects = await subjectRepository.GetCurriculumSubjects(false, default);

        // Assert
        subjects.Should().BeEquivalentTo(_curriculumSubjects);
    }
}
