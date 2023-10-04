using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.Features.YearDataRecords;
using TeachPlanner.WebApi.Tests.Helpers;

namespace TeachPlanner.WebApi.Tests.Features.YearDataRecords;
public class SetSubjectsTaughtTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;
    private readonly SetSubjectsTaught.Validator _validator;

    public SetSubjectsTaughtTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _context = A.Fake<ApplicationDbContext>();
        _validator = A.Fake<SetSubjectsTaught.Validator>();

    }
    [Fact]
    public async void Handle_WhenPassedListOfUntaughtSubjects_ShouldSendWholeListToRepository()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateCurriculumSubjects();
        var teacher = TeacherHelpers.CreateTeacher();
        var handler = new SetSubjectsTaught.Handler(_context, _unitOfWork, _validator);
        var command = new SetSubjectsTaught.Command(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _context.GetTeacherById(teacher.Id, A<CancellationToken>._)).Returns(teacher);
        A.CallTo(() => _context.GetSubjectsById(command.SubjectIds, false, A<CancellationToken>._)).Returns(subjects);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => _context.GetSubjectsById(command.SubjectIds, false, CancellationToken.None)).MustHaveHappenedOnceExactly();
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

        var handler = new SetSubjectsTaught.Handler(_context, _unitOfWork, _validator);
        var command = new SetSubjectsTaught.Command(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _context.GetYearData(teacher.Id, 2023, A<CancellationToken>._)).Returns(yearData);
        A.CallTo(() => _context.GetSubjectsById(command.SubjectIds, false, A<CancellationToken>._)).Returns(subjects);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        yearData.Subjects.Should().BeEquivalentTo(subjects);
    }

    [Fact]
    public async void Handle_WhenNoNewSubjectsAdded_ShouldThrowException()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateCurriculumSubjects().Take(3).ToList();
        var teacher = TeacherHelpers.CreateTeacher();
        var yearData = YearData.Create(teacher.Id, 2023);
        teacher.AddYearData(YearDataEntry.Create(2023, yearData.Id));
        yearData.AddSubjects(subjects);

        var handler = new SetSubjectsTaught.Handler(_context, _unitOfWork, _validator);
        var command = new SetSubjectsTaught.Command(teacher.Id, subjects.Select(s => s.Id).ToList(), 2023);

        A.CallTo(() => _context.GetYearData(teacher.Id, 2023, A<CancellationToken>._)).Returns(yearData);
        A.CallTo(() => _context.GetSubjectsById(command.SubjectIds, false, A<CancellationToken>._)).Returns(subjects);

        // Act
        Func<Task> act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NoNewSubjectsTaughtException>();
    }
}
