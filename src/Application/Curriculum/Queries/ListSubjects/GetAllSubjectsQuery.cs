using ErrorOr;
using MediatR;

namespace Application.Curriculum.Queries.ListSubjects;

public record GetAllSubjectsQuery() : IRequest<ErrorOr<GetAllSubjectsResult>>;
