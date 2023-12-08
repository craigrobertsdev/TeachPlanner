using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Database;
public class TeacherRepositoryTests {
    private readonly IPublisher _publisher;

    public TeacherRepositoryTests() {
        _publisher = A.Fake<IPublisher>();
    }

    private async Task<ApplicationDbContext> GetDbContext() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TeachPlanner")
            .Options;

        var databaseContext = new ApplicationDbContext(options, _publisher);
        databaseContext.Database.EnsureDeleted();
        databaseContext.Database.EnsureCreated();

        if (!await databaseContext.Teachers.AnyAsync()) {
            var teacher1 = Teacher.Create(new UserId(Guid.NewGuid()), "John", "Smith");
            var yearData1 = YearData.Create(teacher1.Id, 2023, DayPlanTemplate.Create(new(), teacher1.Id));
            yearData1.AddSubjects(SubjectHelpers.CreateCurriculumSubjects());
            teacher1.AddYearData(YearDataEntry.Create(2023, yearData1.Id));

            var teacher2 = Teacher.Create(new UserId(Guid.NewGuid()), "Jane", "Smith");
            var yearData2 = YearData.Create(teacher2.Id, 2023, DayPlanTemplate.Create(new(), teacher2.Id));
            yearData2.AddSubjects(SubjectHelpers.CreateCurriculumSubjects());

            databaseContext.Teachers.Add(teacher1);
            databaseContext.Teachers.Add(teacher2);
        }

        // if there are any change tracking issues, uncomment this
        //databaseContext.TermPlanners.AsNoTracking();

        await databaseContext.SaveChangesAsync();
        return databaseContext;
    }
}
