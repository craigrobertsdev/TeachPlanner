using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.LessonPlans.CreateLessonPlan;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Resources;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

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
        private readonly ILessonPlanRepository _lessonPlanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IResourceRepository _resourceRepository;

        public Handler(
            ILessonPlanRepository lessonPlanRepository,
            IAssessmentRepository assessmentRepository,
            IResourceRepository resourceRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _lessonPlanRepository = lessonPlanRepository;
            _assessmentRepository = assessmentRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<CreateLessonPlanResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            List<Assessment> assessments = new();
            if (request.AssessmentIds is not null)
            {
                assessments = await _assessmentRepository.GetAssessmentsById(request.AssessmentIds, cancellationToken);
            }

            List<Resource> resources = new();
            if (request.LessonPlanResources.Count > 0)
            {
                resources = await _resourceRepository.GetResourcesById(request.LessonPlanResources.Select(lr => lr.ResourceId).ToList(), cancellationToken);
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

            _lessonPlanRepository.Add(lesson);

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
