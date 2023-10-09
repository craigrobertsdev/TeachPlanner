using FluentValidation;
using MediatR;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.LessonPlans.CreateLessonPlan;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Features.LessonPlans;

public static class CreateLessonPlan
{
    public record Command(
        YearDataId YearDataId,
        List<Subject> Subjects,
        string PlanningNotes,
        List<LessonPlanResource> LessonPlanResources,
        List<AssessmentId> AssessmentIds,
        DateOnly LessonDate,
        int NumberOfPeriods,
        int StartPeriod) : IRequest<CreateLessonPlanResponse>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.YearDataId).NotEmpty();
            RuleFor(x => x.Subjects).NotEmpty();
            RuleFor(x => x.NumberOfPeriods).NotEmpty();
            RuleFor(x => x.StartPeriod).NotEmpty();
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

            // Add lesson date to request and command. Do validation to make sure that no overlapping lesson plans exist.

            var lesson = LessonPlan.Create(
                request.YearDataId,
                request.SubjectId,
                request.PlanningNotes,
                request.NumberOfPeriods,
                request.LessonPlanResources,
                assessments);

            _lessonPlanRepository.Add(lesson);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateLessonPlanResponse(lesson.Id.Value);
        }
    }

    public static async Task<IResult> Delegate(ISender sender, CreateLessonPlanRequest request, CancellationToken cancellationToken)
    {
        var subjects = request.Subjects.Select(
            s => Subject.Create(
                s.Name,
                s.ContentDescriptionIds.Select(
                    curriculumCode => YearDataContentDescription.Create(curriculumCode)).ToList()
                .ToList()))
            .ToList();

        var command = new Command(
            new YearDataId(request.YearDataId),
            subjects,
            request.PlanningNotes,
            request.LessonPlanResources ?? new(),
            request.AssessmentIds?.Select(id => new AssessmentId(id)).ToList() ?? new(),
            request.LessonDate,
            request.NumberOfPeriods,
            request.StartPeriod);

        var response = await sender.Send(command, cancellationToken);

        return TypedResults.Ok(response);
    }
}
