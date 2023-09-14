using MediatR;

namespace TeachPlanner.Application.Curriculum.Queries.GetAllSubjects;

public record GetAllSubjectsQuery(
    bool Elaborations) : IRequest<GetAllSubjectsResult>;
