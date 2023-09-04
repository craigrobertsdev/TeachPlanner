using ErrorOr;
using MediatR;

namespace TeachPlanner.Application.Curriculum.Queries.GetSubjects;

public record GetSubjectsQuery(
  Guid TeacherId,
  bool Elaborations,
  bool TaughtSubjectsOnly
) : IRequest<ErrorOr<GetSubjectsResult>>;
