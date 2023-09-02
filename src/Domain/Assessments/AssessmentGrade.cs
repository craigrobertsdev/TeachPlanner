using OneOf;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.Assessments;

public class AssessmentGrade : ValueObject
{
    public Grade Grade { get; private set; }
    public double? Percentage { get; private set; }

    private AssessmentGrade(Grade? grade, double? percentage)
    {
        if (percentage is not null)
        {
            Percentage = percentage;
        }

        if (grade is not null)
        {
            Grade = (Grade)grade;
        }
        else
        {
            Grade = FromPercentage();
        }
    }

    public static OneOf<AssessmentGrade, ArgumentException> Create(Grade? grade, double? percentage)
    {
        if (grade is null && percentage is null)
        {
            return new ArgumentException("Grade or Percentage must be provided");
        }

        return new AssessmentGrade(grade, percentage);
    }

    public Grade FromPercentage()
    {
        return Percentage switch
        {
            >= 85 => Grade.A,
            >= 75 => Grade.B,
            >= 65 => Grade.C,
            >= 50 => Grade.D,
            _ => Grade.E
        };
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Grade;
        yield return Percentage;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private AssessmentGrade() { }
}
