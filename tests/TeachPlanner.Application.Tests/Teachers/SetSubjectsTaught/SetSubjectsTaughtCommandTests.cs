using FakeItEasy;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;
using Xunit;

namespace TeachPlanner.Application.Tests.Teachers.SetSubjectsTaught;
public class SetSubjectsTaughtCommandTests
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetSubjectsTaughtCommandTests()
    {
        _teacherRepository = A.Fake<ITeacherRepository>();
        _subjectRepository = A.Fake<ISubjectRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();

    }
    [Fact]
    public async void SetSubjectsTaughtCommndHandler_WhenPassedListOfUntaughtSubjects_ShouldSendWholeListToRepository()
    {
        // Arrange
        var subjects = CreateSubjects();
        var teacher = CreateTeacher();
        var handler = new SetSubjectsTaughtCommandHandler(_teacherRepository, _subjectRepository, _unitOfWork);
        var command = new SetSubjectsTaughtCommand(teacher.Id, subjects.Select(s => s.Id).ToList());

        A.CallTo(() => _teacherRepository.GetById(teacher.Id, A<CancellationToken>._)).Returns(teacher);
        A.CallTo(() => _subjectRepository.GetSubjectsById(command.SubjectIds, A<CancellationToken>._)).Returns(subjects);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => _teacherRepository.SetSubjectsTaughtByTeacher(teacher, subjects)).MustHaveHappenedOnceExactly();
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

    private List<Subject> CreateSubjects()
    {
        List<Subject> subjects = new();

        for (int i = 0; i < 10; i++)
        {
            var subject = Subject.Create("English" + i, new List<YearLevel>());
            var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description" + i, "Achievement Standard", YearLevelValue.Foundation, null);
            var strand = Strand.Create(yearLevel, "Grammar" + i, new List<Substrand>(), null);
            var substrand = Substrand.Create("Grammar constructs" + i, new List<ContentDescription>(), strand);
            var contentDescription = ContentDescription.Create("Description", "ENG001" + i, new List<Elaboration>(), substrand: substrand);

            subject.AddYearLevel(yearLevel);
            yearLevel.AddStrand(strand);
            strand.AddSubstrand(substrand);
            substrand.AddContentDescription(contentDescription);

            subjects.Add(subject);
        }

        return subjects;
    }

    private Teacher CreateTeacher()
    {
        return Teacher.Create("First", "Last", "email", "password");
    }
}
