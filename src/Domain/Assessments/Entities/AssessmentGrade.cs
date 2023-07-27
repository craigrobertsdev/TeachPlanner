using Domain.Assessments.ValueObjects;
using Domain.Common.Enums;
using Domain.Common.Primatives;
using OneOf;

namespace Domain.Assessments.Entities;

public class AssessmentGrade : Entity<GradeId>
{
    public Grade Grade { get; private set; }
    public double? Percentage { get; private set; }

    private AssessmentGrade(GradeId id, Grade? grade, double? percentage) : base(id)
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

        return new AssessmentGrade(new GradeId(Guid.NewGuid()), grade, percentage);
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private AssessmentGrade() { }
}
