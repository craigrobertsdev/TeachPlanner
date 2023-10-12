using FluentValidation;
using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.LessonPlans.CreateLessonPlan;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Features.LessonPlans;

public static class CreateLessonPlan
{
    public record Command(
        YearDataId YearDataId,
        SubjectId SubjectId,
        List<string> CurriculumCodes,
        string PlanningNotes,
        DateOnly LessonDate,
        int NumberOfPeriods,
        int StartPeriod,
        List<LessonPlanResource> LessonPlanResources,
        List<AssessmentId> AssessmentIds) : IRequest<CreateLessonPlanResponse>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.YearDataId).NotNull();
            RuleFor(x => x.SubjectId).NotNull();
            RuleFor(x => x.NumberOfPeriods).NotEmpty();
            RuleFor(x => x.StartPeriod).NotNull();
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

            var lessonPlans = await _lessonPlanRepository.GetByYearDataAndDate(request.YearDataId, request.LessonDate, cancellationToken);

            CheckForConflictingLessonPlans(lessonPlans, request.StartPeriod, request.NumberOfPeriods);

            var lesson = LessonPlan.Create(
                request.YearDataId,
                request.SubjectId,
                request.CurriculumCodes,
                request.PlanningNotes,
                request.NumberOfPeriods,
                request.StartPeriod,
                request.LessonDate,
                request.LessonPlanResources,
                assessments);

            _lessonPlanRepository.Add(lesson);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateLessonPlanResponse(lesson.Id.Value);
        }
    }
    public static async Task<IResult> Delegate(ISender sender, CreateLessonPlanRequest request, CancellationToken cancellationToken)
    {
        var command = new Command(
            new YearDataId(request.YearDataId),
            request.SubjectId,
            request.CurriculumCodes,
            request.PlanningNotes,
            request.LessonDate,
            request.NumberOfPeriods,
            request.StartPeriod,
            request.LessonPlanResources ?? new(),
            request.AssessmentIds?.Select(id => new AssessmentId(id)).ToList() ?? new());

        var response = await sender.Send(command, cancellationToken);

        return TypedResults.Ok(response);
    }

    private static void CheckForConflictingLessonPlans(List<LessonPlan> lessonPlans, int startPeriod, int numberOfPeriods)
    {
        foreach (var lp in lessonPlans)
        {
            if (lp.StartPeriod == startPeriod)
            {
                throw new ConflictingLessonPlansException(lp.StartPeriod);
            }

            if (startPeriod < lp.StartPeriod && startPeriod + numberOfPeriods > lp.StartPeriod)
            {
                throw new ConflictingLessonPlansException(lp.StartPeriod);
            }
        }
    }
}
