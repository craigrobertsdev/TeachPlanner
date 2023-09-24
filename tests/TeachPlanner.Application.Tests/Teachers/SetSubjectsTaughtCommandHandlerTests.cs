using FakeItEasy;
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
    public async void SetSubjectsTaughtCommandHandler_WhenPassedListOfUntaughtSubjects_ShouldSendWholeListToRepository()
    {
        // Arrange
        var subjects = Helpers.CreateSubjects();
        var teacher = Helpers.CreateTeacher();
        var handler = new SetSubjectsTaughtCommandHandler(_teacherRepository, _subjectRepository, _unitOfWork);
        var command = new SetSubjectsTaughtCommand(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _teacherRepository.GetById(teacher.Id, A<CancellationToken>._)).Returns(teacher);
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, A<CancellationToken>._)).Returns(subjects);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => _teacherRepository.SetSubjectsTaughtByTeacher(teacher, subjects, 2023)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, CancellationToken.None)).MustHaveHappenedOnceExactly();
    }

    // I don't know how to test this yet. Teacher is a sealed class with no visible constructors
    // so I can't mock it. 
    // The goal for this test is to test the logic that confirms whether teacher.UpdateSubjectsTaught() 
    // is called with the appropriate subjects
    public async void SetSubjectsTaughtCommandHandler_WhenPassedListWithSomeTaughtSubjects_ShouldCallUpdateSubjectsTaught()
    {
        // Arrange

        // Act

        // Assert

    }


}
