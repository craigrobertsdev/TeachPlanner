using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.WeekPlanners;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.PlannerTemplatesPe;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.WeekPlanners;

namespace TeachPlanner.Api.Features.WeekPlanners;

public static class CreateWeekPlanner
{
    public record Command(
        TeacherId TeacherId,
        WeekPlannerTemplate WeekPlannerTemplate,
        DateTime WeekStart,
        int WeekNumber,
        int TermNumber,
        int Year) : IRequest<WeekPlanner>;
    
    public sealed class Handler : IRequestHandler<Command, WeekPlanner>
    {
        private readonly IWeekPlannerRepository _weekPlannerRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public Handler(IWeekPlannerRepository weekPlannerRepository, ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _weekPlannerRepository = weekPlannerRepository;
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WeekPlanner> Handle(Command request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher is null)
            {
                throw new TeacherNotFoundException();
            }

            var yearDataId = teacher.GetYearData(request.Year);

            if (yearDataId is null)
            {
                throw new YearDataNotFoundException();
            }

            var weekPlanner = WeekPlanner.Create(
                yearDataId,
                request.WeekNumber,
                request.TermNumber,
                request.Year,
                request.WeekPlannerTemplate,
                request.WeekStart);
            
            _weekPlannerRepository.Add(weekPlanner);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return weekPlanner;
        }
    }

    public static async Task<IResult> Delegate(Guid teacherId, CreateWeekPlannerRequest request, ISender sender,
        CancellationToken cancellationToken)
    {
        var weekPlannerTemplate = WeekPlannerTemplate.Create(
            request.WeekPlannerTemplate.DayPlans
                .Select(x => DayPlanTemplate.Create(
                    x.Periods.Select(y => new Period(
                        Enum.Parse<PeriodType>(y.PeriodType),
                        y.StartTime,
                        y.EndTime))
                        .ToList()))
                .ToList());
        
        var command = new Command(
            new TeacherId(teacherId),
            weekPlannerTemplate,
            request.WeekStart,
            request.WeekNumber,
            request.TermNumber,
            request.Year);

        var result = await sender.Send(command, cancellationToken);
        
        return Results.Ok(result);
    }
}
