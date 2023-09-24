using MediatR;

namespace TeachPlanner.Application.Teachers.Queries.GetTeacherSettings;
public record GetTeacherSettingsQuery(Guid TeacherId, int CalendarYear) : IRequest<GetTeacherSettingsResult>;
