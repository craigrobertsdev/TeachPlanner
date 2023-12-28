using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.LessonPlans;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Features.LessonPlans;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests;

public class GetLessonPlanTests {
    private readonly ITeacherRepository _teacherRepository;
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly IYearDataRepository _yearDataRepository;

    public GetLessonPlanTests() {
        _teacherRepository = A.Fake<ITeacherRepository>();
        _curriculumRepository = A.Fake<ICurriculumRepository>();
        _yearDataRepository = A.Fake<IYearDataRepository>();
    }
    [Fact]
    public async void Handle_WhenLessonPlanExists_ReturnLessonPlan() {
        // Arrange
        var teacher = TeacherHelpers.CreateTeacher();
        teacher.AddResource(ResourceHelpers.CreateBasicResource(teacher.Id));
        var yearLevels = TeacherHelpers.CreateYearLevelsTaught();
        var query = new GetDataForBlankLessonPlan.Query(teacher.Id, 2023);
        var subjects = SubjectHelpers.CreateCurriculumSubjects();
        A.CallTo(() => _teacherRepository.GetWithResources(teacher.Id, default)).Returns(teacher);
        A.CallTo(() => _curriculumRepository.GetSubjectsByYearLevels(yearLevels, default)).Returns(subjects);
        var handler = new GetDataForBlankLessonPlan.Handler(_teacherRepository, _curriculumRepository, _yearDataRepository);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        AssertionOptions.FormattingOptions.MaxDepth = 100;
        result.Should().BeOfType<GetLessonPlanDataResponse>();
        result.CurriculumSubjects[0].YearLevels[0].ContentDescriptions[0].CurriculumCode.Should().BeEquivalentTo(subjects[0].YearLevels[0].Strands[0].ContentDescriptions[0].CurriculumCode);
        result.Resources[0].Url.Should().BeEquivalentTo(teacher.Resources[0].Url);
    }

    [Fact]
    public async void Handle_WhenTeacherDoesNotExist_ThrowTeacherNotFoundException() {
        // Arrange
        var teacher = TeacherHelpers.CreateTeacher();
        var yearLevels = TeacherHelpers.CreateYearLevelsTaught();
        var query = new GetDataForBlankLessonPlan.Query(teacher.Id, 2023);
        A.CallTo(() => _teacherRepository.GetWithResources(teacher.Id, default)).Returns((Teacher)null);
        var handler = new GetDataForBlankLessonPlan.Handler(_teacherRepository, _curriculumRepository, _yearDataRepository);

        // Act
        Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<TeacherNotFoundException>();
    }
}
