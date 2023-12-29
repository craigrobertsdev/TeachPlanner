using OneOf;
using TeachPlanner.Shared.Domain.Common.Enums;

namespace TeachPlanner.Shared.Domain.Assessments;

public record AssessmentGrade {
    private AssessmentGrade(Grade? grade, double? percentage) {
        if (percentage is not null) Percentage = percentage;

        if (grade is not null)
            Grade = (Grade)grade;
        else
            Grade = FromPercentage();
    }

    public Grade Grade { get; private set; }
    public double? Percentage { get; }

    public static OneOf<AssessmentGrade, ArgumentException> Create(Grade? grade, double? percentage) {
        if (grade is null && percentage is null) return new ArgumentException("Grade or Percentage must be provided");

        return new AssessmentGrade(grade, percentage);
    }

    public Grade FromPercentage() {
        return Percentage switch {
            >= 85 => Grade.A,
            >= 75 => Grade.B,
            >= 65 => Grade.C,
            >= 50 => Grade.D,
            _ => Grade.E
        };
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private AssessmentGrade() {
    }
}