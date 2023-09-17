using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.Common.Exceptions;
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
        List<Subject> subjects = await _curriculumRepository.GetSubjects(request.Elaborations, cancellationToken);

        if (subjects.Count == 0)
        {
            throw new NoSubjectsFoundException();
        }

        return new GetAllSubjectsResult(subjects);
    }
}
