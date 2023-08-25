using ErrorOr;
using MediatR;

namespace Application.Curriculum.Queries.ListSubjects;

public record GetSubjectsQuery(
  bool Elaborations
) : IRequest<ErrorOr<GetSubjectsResult>>;
