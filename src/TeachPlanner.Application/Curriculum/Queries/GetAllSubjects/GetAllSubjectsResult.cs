using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Curriculum.Queries.GetAllSubjects;

public record GetAllSubjectsResult(List<Subject> Subjects);
