using MediatR;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Features.Teachers.Queries.GetTeacherSettings;
public record GetTeacherSettingsQuery(TeacherId TeacherId, int CalendarYear) : IRequest<GetTeacherSettingsResult>;
