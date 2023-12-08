using FakeItEasy;
using FluentAssertions;
using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Teachers.AccountSetup;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Features.Teachers;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Features.Teachers;
public class AccountSetupTests {
    private readonly DayPlanPatternDto _dayPlanPatternDto;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IYearDataRepository _yearDataRepository;
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;
    private readonly AccountSetup.Validator _validator;

    public AccountSetupTests() {
        _teacherRepository = A.Fake<ITeacherRepository>();
        _yearDataRepository = A.Fake<IYearDataRepository>();
        _curriculumRepository = A.Fake<ICurriculumRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _dayPlanPatternDto = TeacherHelpers.CreateDayPlanPatternDto();
        _sender = A.Fake<ISender>();
        _validator = new();
    }

    //[Fact]
    //public async void Handle_WhenPassedValidData_ShouldCreateNewDayPlanTemplate()
    //{
    //    // Arrange
    //    var subjectsTaught = TeacherHelpers.CreateSubjectNames();
    //    var termDates = TeacherHelpers.CreateTermDates();
    //    var dayPlanTemplate = TeacherHelpers.CreateDayPlanTemplate(_dayPlanPatternDto);
    //    var command = new AccountSetup.Command(subjectsTaught, dayPlanTemplate, termDates, new TeacherId(Guid.NewGuid()),2023);
    //    var handler = new AccountSetup.Handler(_teacherRepository, _curriculumRepository, _yearDataRepository ,_unitOfWork);
    //    var teacher = TeacherHelpers.CreateTeacher();

    //    A.CallTo(() => _teacherRepository.GetById(command.TeacherId, new CancellationToken()))
    //        .Returns(teacher);

    //    A.CallTo(() => _curriculumRepository.GetSubjectsByName(subjectsTaught, new CancellationToken()))
    //        .Returns(TeacherHelpers.CreateCurriculumSubjects(subjectsTaught));

    //    A.CallTo(() => _yearDataRepository.SetInitialAccountDetails(A<Teacher>._, A<List<CurriculumSubject>>._, A<DayPlanTemplateId>._, A<List<TermDate>>._, A<int>._, A<CancellationToken>._))
    //        .Invokes((Teacher t, List<CurriculumSubject> CurriculumSubjects, DayPlanTemplateId dpt, List<TermDate> tds, int cy, CancellationToken ct) =>
    //        {
    //            Assert.Equivalent(teacher, t);
    //            Assert.Equivalent(dayPlanTemplate, dpt);
    //            Assert.Equal(termDates, tds) ;
    //        });

    //    // Act
    //    await handler.Handle(command, new CancellationToken());

    //    // Assert
    //    A.CallTo(() => _teacherRepository.GetById(command.TeacherId, new CancellationToken())).MustHaveHappenedOnceExactly();
    //    A.CallTo(() => _yearDataRepository.SetInitialAccountDetails(teacher, A<List<CurriculumSubject>>._, command.DayPlanTemplateId, command.TermDates, command.CalendarYear ,A<CancellationToken>._))
    //        .MustHaveHappenedOnceExactly();
    //}

    [Fact]
    public void Handle_WhenPassedInvalidPeriodTime_ShouldThrowException() {
        // Arrange
        var subjectsTaught = TeacherHelpers.CreateSubjectNames();
        var yearLevelsTaught = TeacherHelpers.CreateYearLevelsTaught();
        var termDates = TeacherHelpers.CreateTermDates();
        var dayPlanTemplate = TeacherHelpers.CreateDayPlanTemplate(_dayPlanPatternDto);
        dayPlanTemplate.Periods[0].StartTime.AddHours(-9);
        var command = new AccountSetup.Command(subjectsTaught, yearLevelsTaught, dayPlanTemplate, new TeacherId(Guid.NewGuid()), 2023);
        var handler = new AccountSetup.Handler(_teacherRepository, _curriculumRepository, _yearDataRepository, _unitOfWork);

        // Act
        Func<Task> act = () => handler.Handle(command, new CancellationToken());

        // Assert
        act.Should().ThrowAsync<CreateTimeFromDtoException>();
    }

    [Fact]
    public void Handle_WhenPassedOverlappingPeriodTimes_ShouldThrowException() {
        // Arrange
        var subjectsTaught = TeacherHelpers.CreateSubjectNames();
        var yearLevelsTaught = TeacherHelpers.CreateYearLevelsTaught();
        var termDates = TeacherHelpers.CreateTermDates();
        var dayPlanTemplate = TeacherHelpers.CreateDayPlanTemplate(_dayPlanPatternDto);
        dayPlanTemplate.Periods[0].StartTime.AddHours(1);
        var command = new AccountSetup.Command(subjectsTaught, yearLevelsTaught, dayPlanTemplate, new TeacherId(Guid.NewGuid()), 2023);
        var handler = new AccountSetup.Handler(_teacherRepository, _curriculumRepository, _yearDataRepository, _unitOfWork);

        // Act
        Func<Task> act = () => handler.Handle(command, new CancellationToken());

        // Assert
        act.Should().ThrowAsync<CreateTimeFromDtoException>();
    }

    [Fact]
    public void Delegate_WhenPassedInvalidPeriodTime_ShouldThrowException() {
        // Arrange
        var accountSetupRequest = TeacherHelpers.CreateAccountSetupRequestWithOverlappingTimes();

        // Act
        Func<Task> act = () => AccountSetup.Delegate(Guid.NewGuid(), accountSetupRequest, 2023, _sender, _validator, A<CancellationToken>._);

        // Assert
        act.Should().ThrowAsync<CreateTimeFromDtoException>();
    }

    [Fact]
    public void Delegate_WhenPassedOverlappingTermDate_ShouldThrowException() {
        // Arrange
        var accountSetupRequest = TeacherHelpers.CreateAccountSetupRequestWithOverlappingDates();

        // Act
        Func<Task> act = () => AccountSetup.Delegate(Guid.NewGuid(), accountSetupRequest, 2023, _sender, _validator, A<CancellationToken>._);

        // Assert
        act.Should().ThrowAsync<CreateTimeFromDtoException>();
    }

    [Fact]
    public async void Delegate_WhenPassedValidData_ShouldCreateDayPlanTemplate() {
        // Arrange
        var accountSetupRequest = TeacherHelpers.CreateAccountSetupRequest();
        var sender = A.Fake<ISender>();

        // Act
        await AccountSetup.Delegate(Guid.NewGuid(), accountSetupRequest, 2023, sender, _validator, CancellationToken.None);

        // Assert
        var call = Fake.GetCalls(sender).First();
        AccountSetup.Command command = (AccountSetup.Command)call.Arguments[0]!;
        command.DayPlanTemplate.Periods.Should().Equal(TeacherHelpers.CreateDayPlanTemplate(_dayPlanPatternDto).Periods);
    }

    [Fact]
    public async void Handler_WhenPassedValidData_ShouldUpdateTeacherSubjectsTaught() {
        // Arrange
        var subjects = TeacherHelpers.CreateSubjectNames();
        var dayPlanTemplate = TeacherHelpers.CreateDayPlanTemplate(_dayPlanPatternDto);
        var yearLevelsTaught = TeacherHelpers.CreateYearLevelsTaught();
        var termDates = TeacherHelpers.CreateTermDates();
        var teacher = TeacherHelpers.CreateTeacher();
        var subjectsTaught = TeacherHelpers.CreateCurriculumSubjects(subjects);

        A.CallTo(() => _teacherRepository.GetById(teacher.Id, A<CancellationToken>._))
            .Returns(teacher);

        A.CallTo(() => _curriculumRepository.GetSubjectsByName(subjects, A<CancellationToken>._))
            .Returns(subjectsTaught);

        var command = new AccountSetup.Command(subjects, yearLevelsTaught, dayPlanTemplate, teacher.Id, 2023);
        var handler = new AccountSetup.Handler(_teacherRepository, _curriculumRepository, _yearDataRepository, _unitOfWork);

        // Act
        await handler.Handle(command, new CancellationToken());

        // Assert
        teacher.SubjectsTaught.Should().Equal(subjectsTaught);
    }
}
