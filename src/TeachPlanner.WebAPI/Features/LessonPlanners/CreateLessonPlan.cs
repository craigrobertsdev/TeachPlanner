using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.LessonPlannner.CreateLessonPlan;
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
        List<ResourceId>? ResourceIds,
        List<AssessmentId>? AssessmentIds,
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
            if (request.ResourceIds != null)
            {
                resources = await _context.Resources
                    .Where(x => request.ResourceIds.Contains(x.Id))
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
                resources,
                assessments);

            _context.LessonPlans.Add(lesson);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateLessonPlanResponse(lesson.Id.Value);


        }
    }
}
