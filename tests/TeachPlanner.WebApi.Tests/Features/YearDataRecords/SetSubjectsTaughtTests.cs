using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.Features.YearDataRecords;
using TeachPlanner.WebApi.Tests.Helpers;

namespace TeachPlanner.WebApi.Tests.Features.YearDataRecords;
public class SetSubjectsTaughtTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IYearDataRepository _yearDataRepository;
    private readonly ISubjectRepository _subjectRepository;

    public SetSubjectsTaughtTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _subjectRepository = A.Fake<ISubjectRepository>();
        _yearDataRepository = A.Fake<IYearDataRepository>();

    }
    [Fact]
    public async void Handle_WhenPassedListOfUntaughtSubjects_ShouldSendWholeListToRepository()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateCurriculumSubjects();
        var yearData = YearDataHelpers.CreateYearData();
        var teacher = TeacherHelpers.CreateTeacher();
        var handler = new SetSubjectsTaught.Handler(_yearDataRepository, _subjectRepository, _unitOfWork);
        var command = new SetSubjectsTaught.Command(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _yearDataRepository.GetByTeacherIdAndYear(teacher.Id, 2023, A<CancellationToken>._)).Returns(yearData);
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, false, A<CancellationToken>._)).Returns(subjects);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, false, CancellationToken.None)).MustHaveHappenedOnceExactly();
        yearData.Subjects.Should().BeEquivalentTo(subjects);
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

        var handler = new SetSubjectsTaught.Handler(_yearDataRepository, _subjectRepository, _unitOfWork);
        var command = new SetSubjectsTaught.Command(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _yearDataRepository.GetByTeacherIdAndYear(teacher.Id, 2023, A<CancellationToken>._)).Returns(yearData);
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, false, A<CancellationToken>._)).Returns(subjects);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        yearData.Subjects.Should().BeEquivalentTo(subjects);
    }
}
