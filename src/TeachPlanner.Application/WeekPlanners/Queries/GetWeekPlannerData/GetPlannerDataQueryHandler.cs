using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.WeekPlanners.Queries.GetPlannerData;

public class GetPlannerDataQueryHandler //: IRequestHandler<GetPlannerDataQuery, ErrorOr<GetPlannerDataResult>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISubjectRepository _subjectRepository;

    public GetPlannerDataQueryHandler(
        ITeacherRepository teacherRepository,
        ISubjectRepository subjectRepository)
    {
        _teacherRepository = teacherRepository;
        _subjectRepository = subjectRepository;
    }

    // public async Task<ErrorOr<GetPlannerDataResult>> Handle(GetPlannerDataQuery request, CancellationToken cancellationToken)
    // {
    //     var teacher = await _teacherRepository.GetById(request.TeacherId);

    //     if (teacher is null)
    //     {
    //         return new NotFoundError($"Teachers with id {request.TeacherId} not found");
    //     }

    //     var weekPlanner = await _weekPlannerRepository.GetWeekPlanner(teacher.WeekPlannerId);

    //     if (weekPlanner is null)
    //     {
    //         return new NotFoundError($"WeekPlanners with id {teacher.WeekPlannerId} not found");
    //     }

    //     var dayPlanPattern = await _dayPlanPatternRepository.GetDayPlanPattern(weekPlanner.DayPlanPatternId);

    //     if (dayPlanPattern is null)
    //     {
    //         return new NotFoundError($"DayPlanPattern with id {weekPlanner.DayPlanPatternId} not found");
    //     }

    //     var subjects = await _subjectRepository.GetAllSubjects(teacher.SubjectIds);

    //     return new GetPlannerDataResult(weekPlanner, dayPlanPattern, subjects);
    // // }
}