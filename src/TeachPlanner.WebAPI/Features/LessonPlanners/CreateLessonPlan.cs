using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.LessonPlans.CreateLessonPlan;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Entities.Assessments;
using TeachPlanner.Api.Entities.LessonPlans;
using TeachPlanner.Api.Entities.Resources;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.Teachers;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Features.LessonPlanners;

public static class CreateLessonPlan
{
    public record Command(
        Guid TeacherId,
        Guid SubjectId,
        string PlanningNotes,
        List<LessonPlanResource> LessonPlanResources,
        List<AssessmentId> AssessmentIds,
        DateTime StartTime,
        DateTime EndTime,
        int NumberOfPeriods) : IRequest<CreateLessonPlanResponse>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.TeacherId).NotEmpty();
            RuleFor(x => x.SubjectId).NotEmpty();
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty();
            RuleFor(x => x.NumberOfPeriods).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, CreateLessonPlanResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateLessonPlanResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            List<Assessment> assessments = new();
            if (request.AssessmentIds != null)
            {
                assessments = await _context.Assessments
                    .Where(x => request.AssessmentIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);
            }

            List<Resource> resources = new();
            if (request.LessonPlanResources.Count > 0)
            {
                resources = await _context.Resources
                    .Where(x => request.LessonPlanResources.Select(lr => lr.ResourceId).Contains(x.Id))
                    .ToListAsync(cancellationToken);
            }

            var lesson = LessonPlan.Create(
                new TeacherId(request.TeacherId),
                new YearDataId(Guid.NewGuid()),
                new SubjectId(request.SubjectId),
                request.PlanningNotes,
                request.StartTime,
                request.EndTime,
                request.NumberOfPeriods,
                assessments);

            _context.LessonPlans.Add(lesson);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateLessonPlanResponse(lesson.Id.Value);
        }
    }

    public static async Task<IResult> Delegate(ISender sender, Guid teacherId, CreateLessonPlanRequest request, CancellationToken cancellationToken)
    {
        var command = new Command(
            teacherId,
            request.SubjectId,
            request.PlanningNotes,
            request.LessonPlanResources ?? new(),
            request.AssessmentIds?.Select(id => new AssessmentId(id)).ToList() ?? new(),
            request.StartTime,
            request.EndTime,
            request.NumberOfPeriods);

        var response = await sender.Send(command, cancellationToken);

        return TypedResults.Ok(response);
    }
}
