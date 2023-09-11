using MediatR;
using TeachPlanner.Application.Common.Errors;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.Curriculum.Queries.GetSubjects;
public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, GetSubjectsResult>
{
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly ITeacherRepository _teacherRepository;

    public GetSubjectsQueryHandler(ICurriculumRepository curriculumRepository, ITeacherRepository teacherRepository)
    {
        _curriculumRepository = curriculumRepository;
        _teacherRepository = teacherRepository;
    }
    public async Task<GetSubjectsResult> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherById(request.TeacherId);

        if (teacher == null)
        {
            throw new TeacherNotFoundException();
        }


        var subjects = request.TaughtSubjectsOnly ?
            await _teacherRepository.GetSubjectsTaughtByTeacher(teacher.Id, request.Elaborations)
            : await _curriculumRepository.GetSubjects(request.Elaborations);

        if (subjects == null)
        {
            throw new TeacherHasNoSubjectsException();
        }

        return new GetSubjectsResult(subjects);
    }
}
