using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Features.Subjects;

public static class GetCurriculumSubjects
{
    public record Query(bool IncludeElaborations) : IRequest<List<CurriculumSubject>>;

    public sealed class Handler : IRequestHandler<Query, List<CurriculumSubject>>
    {
        private readonly ISubjectRepository _subjectRepository;

        public Handler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<List<CurriculumSubject>> Handle(Query request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetCurriculumSubjects(request.IncludeElaborations, cancellationToken);

            return subjects;
        }
    }

    public async static Task<IResult> Delegate(bool includeElaborations, ISender sender, CancellationToken cancellationToken)
    {
        var query = new Query(includeElaborations);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}
