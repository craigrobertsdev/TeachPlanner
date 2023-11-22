using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Common.Interfaces.Services;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.Features.YearDataRecords;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Features.YearDataRecords;
public class SetSubjectsTaughtTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IYearDataRepository _yearDataRepository;
    private readonly ICurriculumService _curriculumService;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ICurriculumRepository _curriculumRepository;

    public SetSubjectsTaughtTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _subjectRepository = A.Fake<ISubjectRepository>();
        _yearDataRepository = A.Fake<IYearDataRepository>();
        _curriculumService = A.Fake<ICurriculumService>();
        _curriculumRepository = A.Fake<ICurriculumRepository>();
    }

    [Fact]
    public async void Handle_WhenPassedListOfUntaughtSubjects_ShouldSendWholeListToRepository()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateCurriculumSubjects();
        var yearData = YearDataHelpers.CreateYearData();
        var teacher = TeacherHelpers.CreateTeacher();
        var handler = new SetSubjectsTaught.Handler(_yearDataRepository, _subjectRepository, _unitOfWork, _curriculumService);
        var command = new SetSubjectsTaught.Command(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _yearDataRepository.GetByTeacherIdAndYear(teacher.Id, 2023, A<CancellationToken>._)).Returns(yearData);
        A.CallTo(() => _curriculumService.CurriculumSubjects).Returns(subjects);    

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        yearData.Subjects.Count.Should().Be(subjects.Count);
    }

    [Fact]
    public async void Handle_WhenPassedListWithSomeNewSubjects_ShouldOnlyAddNewSubjects()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateCurriculumSubjects();
        var teacher = TeacherHelpers.CreateTeacher();
        var yearData = YearData.Create(teacher.Id, 2023);
        teacher.AddYearData(YearDataEntry.Create(2023, yearData.Id));
        yearData.AddSubjects(subjects.Take(3).ToList());

        var handler = new SetSubjectsTaught.Handler(_yearDataRepository, _subjectRepository, _unitOfWork, _curriculumService);
        var command = new SetSubjectsTaught.Command(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _yearDataRepository.GetByTeacherIdAndYear(teacher.Id, 2023, A<CancellationToken>._)).Returns(yearData);
        A.CallTo(() => _curriculumService.CurriculumSubjects).Returns(subjects);    

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        yearData.Subjects.Count.Should().Be(subjects.Count);
    }
}
