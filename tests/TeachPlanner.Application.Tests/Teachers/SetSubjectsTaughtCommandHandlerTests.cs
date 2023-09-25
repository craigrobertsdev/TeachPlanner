using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
using Xunit;

namespace TeachPlanner.Application.Tests.Teachers;
public class SetSubjectsTaughtCommandHandlerTests
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetSubjectsTaughtCommandHandlerTests()
    {
        _teacherRepository = A.Fake<ITeacherRepository>();
        _subjectRepository = A.Fake<ISubjectRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();

    }
    [Fact]
    public async void Handle_WhenPassedListOfUntaughtSubjects_ShouldSendWholeListToRepository()
    {
        // Arrange
        var subjects = Helpers.CreateCurriculumSubjects();
        var teacher = Helpers.CreateTeacher();
        var handler = new SetSubjectsTaughtCommandHandler(_teacherRepository, _subjectRepository, _unitOfWork);
        var command = new SetSubjectsTaughtCommand(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _teacherRepository.GetById(teacher.Id, A<CancellationToken>._)).Returns(teacher);
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, A<CancellationToken>._)).Returns(subjects);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, CancellationToken.None)).MustHaveHappenedOnceExactly();
        teacher.GetYearData(command.CalendarYear)!.Subjects.Should().BeEquivalentTo(subjects);
    }

    [Fact]
    public async void Handle_WhenPassedListWithSomeTaughtSubjects_ShouldCallUpdateSubjectsTaught()
    {
        // Arrange
        var subjects = Helpers.CreateCurriculumSubjects();
        var teacher = Helpers.CreateTeacher();
        teacher.AddYearData(2023);
        teacher.GetYearData(2023)!.AddSubjects(subjects.Take(3).ToList());

        var handler = new SetSubjectsTaughtCommandHandler(_teacherRepository, _subjectRepository, _unitOfWork);
        var command = new SetSubjectsTaughtCommand(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _teacherRepository.GetById(teacher.Id, A<CancellationToken>._)).Returns(teacher);
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, A<CancellationToken>._)).Returns(subjects);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        teacher.GetYearData(command.CalendarYear)!.Subjects.Should().BeEquivalentTo(subjects);
    }

    [Fact]
    public async void Handle_WhenNoNewSubjectsAdded_ShouldNotCallSaveChanges()
    {

    }
}
