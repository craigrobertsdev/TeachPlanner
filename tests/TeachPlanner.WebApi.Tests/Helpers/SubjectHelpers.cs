using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.WebApi.Tests.Helpers;
public static class SubjectHelpers
{
    public static List<Subject> CreateCurriculumSubjects()
    {
        List<Subject> subjects = new();

        for (int i = 0; i < 10; i++)
        {
            var subject = Subject.CreateCurriculumSubject("English" + i, new List<YearLevel>());
            var yearLevel = YearLevel.Create(new List<Strand>(), "Description" + i, "Achievement Standard", YearLevelValue.Foundation, null);
            var strand = Strand.Create("Grammar" + i, new List<ContentDescription>());
            var contentDescription = ContentDescription.Create("Description", "ENG001" + i, new List<Elaboration>());

            subject.AddYearLevel(yearLevel);
            yearLevel.AddStrand(strand);
            strand.AddContentDescription(contentDescription);

            subjects.Add(subject);
        }

        return subjects;
    }
    public static List<Subject> CreateSubjects()
    {
        List<Subject> subjects = new();

        for (int i = 0; i < 10; i++)
        {
            var subject = Subject.Create("English" + i, new List<YearLevel>());
            var yearLevel = YearLevel.Create(new List<Strand>(), "Description" + i, "Achievement Standard", YearLevelValue.Foundation, null);
            var strand = Strand.Create("Grammar" + i, new List<ContentDescription>());
            var contentDescription = ContentDescription.Create("Description", "ENG001" + i, new List<Elaboration>());

            subject.AddYearLevel(yearLevel);
            yearLevel.AddStrand(strand);
            strand.AddContentDescription(contentDescription);

            subjects.Add(subject);
        }

        return subjects;
    }
}
