using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Domain.Teachers;
using TeachPlanner.Domain.Tests.Helpers;
using TeachPlanner.Domain.YearDataRecord;
using TeachPlanner.Infrastructure.Persistence.DbContexts;
using TeachPlanner.Infrastructure.Persistence.Repositories;

namespace TeachPlanner.Infrastructure.Tests.TeacherRespository;
public class TeacherRepositoryTests
{
    private async Task<ApplicationDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TeachPlanner")
            .Options;

        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureDeleted();
        databaseContext.Database.EnsureCreated();

        if (!(await databaseContext.Teachers.AnyAsync()))
        {
            var john = Teacher.Create(Guid.NewGuid(), "John", "Smith");
            var yearData1 = YearData.Create(2023);
            yearData1.AddSubjects(SubjectHelpers.CreateCurriculumSubjects());
            john.AddYearData(yearData1);

            var jane = Teacher.Create(Guid.NewGuid(), "Jane", "Smith");
            var yearData2 = YearData.Create(2023);
            yearData2.AddSubjects(SubjectHelpers.CreateCurriculumSubjects());

            databaseContext.Teachers.Add(john);
            databaseContext.Teachers.Add(jane);
        }

        // if there are any change tracking issues, uncomment this
        //databaseContext.TermPlanners.AsNoTracking();

        await databaseContext.SaveChangesAsync();
        return databaseContext;
    }
    [Fact]
    public async void GetTeacher_WhenCalled_ShouldHaveAssociatedData()
    {
        // Arrange
        var dbContext = await GetDbContext();
        var teacherRepository = new TeacherRepository(dbContext);

        // Act
        var teacher = await teacherRepository.GetById(dbContext.Teachers.First().Id, new CancellationToken());

        // Assert 
        teacher.Should().NotBeNull();
        teacher!.YearDataHistory.Should().NotBeEmpty();
    }

}
