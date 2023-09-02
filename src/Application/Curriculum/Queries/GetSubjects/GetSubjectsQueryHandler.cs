using ErrorOr;
using MediatR;
using TeachPlanner.Application.Common.Errors;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.Curriculum.Queries.GetSubjects;
public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, ErrorOr<GetSubjectsResult>>
{
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly ITeacherRepository _teacherRepository;

    public GetSubjectsQueryHandler(ICurriculumRepository curriculumRepository, ITeacherRepository teacherRepository)
    {
        _curriculumRepository = curriculumRepository;
        _teacherRepository = teacherRepository;
    }
    public async Task<ErrorOr<GetSubjectsResult>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var subjectsTaught = await _teacherRepository.GetSubjectsTaughtByTeacher(request.TeacherId);

        if (subjectsTaught == null)
        {
            return Errors.Teacher.TeacherNotFound;
        }

        var subjects = request.Elaborations == true ?
            await _curriculumRepository.GetSubjects(subjectsTaught) :
            await _curriculumRepository.GetSubjectsWithoutElaborations(subjectsTaught);

        return new GetSubjectsResult(subjects);
    }
}
