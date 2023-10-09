using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.UnitTests.Helpers;
internal static class TermPlannerHelpers
{
    internal static TermPlanner CreateTermPlanner()
    {
        return TermPlanner.Create(new YearDataId(Guid.NewGuid()), 2023, new List<YearLevelValue> { YearLevelValue.Foundation, YearLevelValue.Year1 });
    }

    internal static TermPlan CreateTermPlan(TermPlanner termPlanner, string curriculumCode, bool withSubstrands = true)
    {
        var subjects = new List<CurriculumSubject>()
        {
            CreateSubject("English", curriculumCode)
        };
        return TermPlan.Create(termPlanner, 1, subjects);
    }

    internal static CurriculumSubject CreateSubject(string name, string curriculumCode)
    {
        var subject = CurriculumSubject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create("Grammar", new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static CurriculumSubject CreateSubject(string name, string curriculumCode, YearLevelValue subjectYearLevel)
    {
        var subject = CurriculumSubject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", subjectYearLevel, null);
        var strand = Strand.Create("Grammar", new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static CurriculumSubject CreateSubject(string name, string curriculumCode, string strandName)
    {
        var subject = CurriculumSubject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create(strandName, new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static CurriculumSubject CreateSubject(string name, string curriculumCode, string strandName, string substrandName)
    {
        var subject = CurriculumSubject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create(strandName, new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddContentDescription(contentDescription);

        return subject;
    }

}
