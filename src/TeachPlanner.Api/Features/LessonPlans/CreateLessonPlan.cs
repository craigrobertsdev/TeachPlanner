using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Contracts.LessonPlans.CreateLessonPlan;
using TeachPlanner.Shared.Domain.Assessments;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.LessonPlans;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Api.Features.LessonPlans;

public static class CreateLessonPlan {
    public static async Task<IResult> Delegate([FromRoute] Guid teacherId, ISender sender, CreateLessonPlanRequest request,
        CancellationToken cancellationToken) {
        var command = new Command(
            new TeacherId(teacherId),
            request.SubjectId,
            request.CurriculumCodes,
            request.PlanningNotes,
            request.LessonDate,
            request.NumberOfPeriods,
            request.StartPeriod,
            request.LessonPlanResources ?? new List<LessonPlanResource>(),
            request.AssessmentIds?.Select(id => new AssessmentId(id)).ToList() ?? new List<AssessmentId>());

        var response = await sender.Send(command, cancellationToken);

        return TypedResults.Ok(response);
    }

    public record Command(
        TeacherId TeacherId,
        SubjectId SubjectId,
        List<string> CurriculumCodes,
        string PlanningNotes,
        DateOnly LessonDate,
        int NumberOfPeriods,
        int StartPeriod,
        List<LessonPlanResource> LessonPlanResources,
        List<AssessmentId> AssessmentIds) : IRequest<CreateLessonPlanResponse>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(x => x.SubjectId).NotNull();
            RuleFor(x => x.NumberOfPeriods).NotEmpty();
            RuleFor(x => x.StartPeriod).NotNull();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, CreateLessonPlanResponse> {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILessonPlanRepository _lessonPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ILessonPlanRepository lessonPlanRepository, IAssessmentRepository assessmentRepository, ITeacherRepository teacherRepository, IUnitOfWork unitOfWork) {
            _lessonPlanRepository = lessonPlanRepository;
            _assessmentRepository = assessmentRepository;
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateLessonPlanResponse> Handle(Command request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);
            if (teacher is null) {
                throw new TeacherNotFoundException();
            }

            var yearDataId = teacher.GetYearData(request.LessonDate.Year);
            if (yearDataId is null) {
                throw new YearDataNotFoundException();
            }

            List<Assessment> assessments = new();
            if (request.AssessmentIds is not null)
                assessments = await _assessmentRepository.GetAssessmentsById(request.AssessmentIds, cancellationToken);

            var lessonPlans = await _lessonPlanRepository.GetByYearDataAndDate(yearDataId!, request.LessonDate, cancellationToken);
            CheckForConflictingLessonPlans(lessonPlans, request.StartPeriod, request.NumberOfPeriods);

            var lesson = LessonPlan.Create(
                yearDataId,
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

        private static void CheckForConflictingLessonPlans(List<LessonPlan> lessonPlans, int startPeriod,
            int numberOfPeriods) {
            foreach (var lp in lessonPlans) {
                if (lp.StartPeriod == startPeriod) throw new ConflictingLessonPlansException(lp.StartPeriod);

                if (startPeriod < lp.StartPeriod && startPeriod + numberOfPeriods > lp.StartPeriod)
                    throw new ConflictingLessonPlansException(lp.StartPeriod);
            }
        }
    }
}