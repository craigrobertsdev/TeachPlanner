using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace Application.Curriculum.Queries.ListSubjects;
public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, ErrorOr<GetAllSubjectsResult>>
{
    private readonly ICurriculumRepository _curriculumRepository;

    public GetAllSubjectsQueryHandler(ICurriculumRepository curriculumRepository)
    {
        _curriculumRepository = curriculumRepository;
    }
    public async Task<ErrorOr<GetAllSubjectsResult>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        var subjects = await _curriculumRepository.GetAllSubjects();

        return new GetAllSubjectsResult(subjects);
    }
}
