using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.UnitTests.Database;
public class LessonPlanRespositoryTests
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
            var lessonPlan = LessonPlan.Create(
                new YearDataId(Guid.NewGuid()),
                new SubjectId(Guid.NewGuid()),
                "Planning Notes",
                DateTime.Now,
                DateTime.Now.AddHours(1),
                1,
                new List<LessonPlanResource>(),
                new List<Assessment>());

            databaseContext.LessonPlans.Add(lessonPlan);
        }

        // if there are any change tracking issues, uncomment this
        //databaseContext.TermPlanners.AsNoTracking();

        await databaseContext.SaveChangesAsync();

        return databaseContext;
    }
}
