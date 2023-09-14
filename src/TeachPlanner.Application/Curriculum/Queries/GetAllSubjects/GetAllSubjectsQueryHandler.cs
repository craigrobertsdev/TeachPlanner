using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Curriculum.Queries.GetAllSubjects;
public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, GetAllSubjectsResult>
{
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly ITeacherRepository _teacherRepository;

    public GetAllSubjectsQueryHandler(ICurriculumRepository curriculumRepository, ITeacherRepository teacherRepository)
    {
        _curriculumRepository = curriculumRepository;
        _teacherRepository = teacherRepository;
    }
    public async Task<GetAllSubjectsResult> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        List<Subject> subjects = await _curriculumRepository.GetSubjects(request.Elaborations);

        if (subjects == null)
        {
            throw new TeacherHasNoSubjectsException();
        }

        return new GetAllSubjectsResult(subjects);
    }
}
