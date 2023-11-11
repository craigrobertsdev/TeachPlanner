using FakeItEasy;
using FluentAssertions;
using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Teachers.AccountSetup;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Features.Teachers;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Features.Teachers;
public class AccountSetupTests
{
    private readonly DayPlanPatternDto _dayPlanPatternDto;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;
    private readonly AccountSetup.Validator _validator;

    public AccountSetupTests()
    {
        _teacherRepository = A.Fake<ITeacherRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _dayPlanPatternDto = TeacherHelpers.CreateDayPlanPatternDto();
        _sender = A.Fake<ISender>();
        _validator = new();
    }

    [Fact]
    public async void Handle_WhenPassedValidData_ShouldCreateNewDayPlanTemplate()
    {
        // Arrange
        var subjectsTaught = TeacherHelpers.CreateSubjectsTaught();
        var termDates = TeacherHelpers.CreateTermDates();
        var dayPlanTemplate = TeacherHelpers.CreateDayPlanTemplate(_dayPlanPatternDto);
        var command = new AccountSetup.Command(subjectsTaught, dayPlanTemplate, termDates, new TeacherId(Guid.NewGuid()));
        var handler = new AccountSetup.Handler(_teacherRepository, _unitOfWork);
        var teacher = TeacherHelpers.CreateTeacher();

        A.CallTo(() => _teacherRepository.GetById(command.TeacherId, new CancellationToken()))
            .Returns(teacher);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        A.CallTo(() => _teacherRepository.GetById(command.TeacherId, new CancellationToken())).MustHaveHappenedOnceExactly();
        A.CallTo(() => _teacherRepository.SetInitialAccountDetails(teacher.Id, command.SubjectsTaught, command.DayPlanTemplate, command.TermDates, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Handle_WhenPassedInvalidPeriodTime_ShouldThrowException()
    {
        // Arrange
        var subjectsTaught = TeacherHelpers.CreateSubjectsTaught();
        var termDates = TeacherHelpers.CreateTermDates();
        var dayPlanTemplate = TeacherHelpers.CreateDayPlanTemplate(_dayPlanPatternDto);
        dayPlanTemplate.Periods[0].StartTime.AddHours(-9);
        var command = new AccountSetup.Command(subjectsTaught, dayPlanTemplate, termDates, new TeacherId(Guid.NewGuid()));
        var handler = new AccountSetup.Handler(_teacherRepository, _unitOfWork);

        // Act
        Func<Task> act = () => handler.Handle(command, new CancellationToken());

        // Assert
        act.Should().ThrowAsync<CreateTimeFromDtoException>();  
    }

    [Fact]
    public void Handle_WhenPassedOverlappingPeriodTimes_ShouldThrowException()
    {
        // Arrange
        var subjectsTaught = TeacherHelpers.CreateSubjectsTaught();
        var termDates = TeacherHelpers.CreateTermDates();
        var dayPlanTemplate = TeacherHelpers.CreateDayPlanTemplate(_dayPlanPatternDto);
        dayPlanTemplate.Periods[0].StartTime.AddHours(1);
        var command = new AccountSetup.Command(subjectsTaught, dayPlanTemplate, termDates, new TeacherId(Guid.NewGuid()));
        var handler = new AccountSetup.Handler(_teacherRepository, _unitOfWork);

        // Act
        Func<Task> act = () => handler.Handle(command, new CancellationToken());

        // Assert
        act.Should().ThrowAsync<CreateTimeFromDtoException>();  
    }

    [Fact]
    public void Delegate_WhenPassedInvalidPeriodTime_ShouldThrowException()
    {
        // Arrange
        var accountSetupRequest = TeacherHelpers.CreateAccountSetupRequestWithOverlappingTimes();

        // Act
        Func<Task> act = () => AccountSetup.Delegate(Guid.NewGuid(), accountSetupRequest, _sender, _validator);

        // Assert
        act.Should().ThrowAsync<CreateTimeFromDtoException>();
    }

    [Fact]
    public void Delegate_WhenPassedOverlappingTermDate_ShouldThrowException()
    {
        // Arrange
        var accountSetupRequest = TeacherHelpers.CreateAccountSetupRequestWithOverlappingDates();

        // Act
        Func<Task> act = () => AccountSetup.Delegate(Guid.NewGuid(), accountSetupRequest, _sender, _validator);

        // Assert
        act.Should().ThrowAsync<CreateTimeFromDtoException>();
    }
}
