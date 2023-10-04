using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Features.Subjects;

public static class GetCurriculumSubjects
{
    public record Query(bool IncludeElaborations) : IRequest<List<Subject>>;

    public sealed class Handler : IRequestHandler<Query, List<Subject>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subject>> Handle(Query request, CancellationToken cancellationToken)
        {
            var subjects = await _context.GetCurriculumSubjects(request.IncludeElaborations, cancellationToken);

            return subjects;
        }
    }

    public async static Task<IResult> Delegate(bool includeElaborations, ISender sender)
    {
        var query = new Query(includeElaborations);

        var result = await sender.Send(query, new CancellationToken());

        return Results.Ok(result);
    }
}
