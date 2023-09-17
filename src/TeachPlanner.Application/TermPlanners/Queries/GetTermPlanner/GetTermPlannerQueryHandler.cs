using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
public class GetTermPlannerQueryHandler : IRequestHandler<GetTermPlannerQuery, GetTermPlannerResult>
{
    private readonly ITermPlannerRepository _repository;

    public GetTermPlannerQueryHandler(ITermPlannerRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetTermPlannerResult> Handle(GetTermPlannerQuery request, CancellationToken cancellationToken)
    {
        //var teacher = _teacherRepository.GetById
        var termPlanner = await _repository.Get(request.TermPlannerId, cancellationToken);

        if (termPlanner == null)
        {
            throw new TermPlannerNotFoundException();
        }

        if (termPlanner.TeacherId != request.TeacherId)
        {
            throw new TermPlannerDoesNotBelongToTeacherException();
        }

        return new GetTermPlannerResult(termPlanner);
    }
}
