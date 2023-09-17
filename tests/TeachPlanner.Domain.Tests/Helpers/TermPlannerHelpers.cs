using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.TermPlanners;

namespace TeachPlanner.Domain.Tests.Helpers;
internal static class TermPlannerHelpers
{
    internal static TermPlanner CreateTermPlanner()
    {
        return TermPlanner.Create(2023, YearLevelValue.Foundation, null);
    }

    internal static TermPlan CreateTermPlan(TermPlanner termPlanner, string curriculumCode, bool withSubstrands = true)
    {
        var subjects = new List<Subject>()
        {
            withSubstrands ? CreateSubject("English", curriculumCode) : CreateSubjectWithoutSubstrands("Maths", curriculumCode)
        };
        return TermPlan.Create(termPlanner, 1, subjects);
    }

    internal static Subject CreateSubject(string name, string curriculumCode)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create("Grammar", yearLevel, new List<Substrand>(), null);
        var substrand = Substrand.Create("Grammar constructs", new List<ContentDescription>(), strand);
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>(), substrand: substrand);

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddSubstrand(substrand);
        substrand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static Subject CreateSubjectWithoutSubstrands(string name, string curriculumCode)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create("Grammar", yearLevel, null, new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>(), strand: strand);

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static Subject CreateSubject(string name, string curriculumCode, YearLevelValue subjectYearLevel)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description", "Achievement Standard", subjectYearLevel, null);
        var strand = Strand.Create("Grammar", yearLevel, new List<Substrand>(), null);
        var substrand = Substrand.Create("Grammar constructs", new List<ContentDescription>(), strand);
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>(), substrand: substrand);

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddSubstrand(substrand);
        substrand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static Subject CreateSubject(string name, string curriculumCode, string strandName)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create(strandName, yearLevel, new List<Substrand>(), null);
        var substrand = Substrand.Create("Grammar constructs", new List<ContentDescription>(), strand);
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>(), substrand: substrand);

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddSubstrand(substrand);
        substrand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static Subject CreateSubject(string name, string curriculumCode, string strandName, string substrandName)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create(strandName, yearLevel, new List<Substrand>(), null);
        var substrand = Substrand.Create(substrandName, new List<ContentDescription>(), strand);
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>(), substrand: substrand);

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddSubstrand(substrand);
        substrand.AddContentDescription(contentDescription);

        return subject;
    }

}
