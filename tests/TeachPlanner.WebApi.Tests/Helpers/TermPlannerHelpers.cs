﻿using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.WebApi.Tests.Helpers;
internal static class TermPlannerHelpers
{
    internal static TermPlanner CreateTermPlanner()
    {
        return TermPlanner.Create(new YearDataId(Guid.NewGuid()), 2023, new List<YearLevelValue> { YearLevelValue.Foundation, YearLevelValue.Year1 });
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
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create("Grammar", new List<Substrand>(), null);
        var substrand = Substrand.Create("Grammar constructs", new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddSubstrand(substrand);
        substrand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static Subject CreateSubjectWithoutSubstrands(string name, string curriculumCode)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create("Grammar", null, new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static Subject CreateSubject(string name, string curriculumCode, YearLevelValue subjectYearLevel)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", subjectYearLevel, null);
        var strand = Strand.Create("Grammar", new List<Substrand>(), null);
        var substrand = Substrand.Create("Grammar constructs", new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddSubstrand(substrand);
        substrand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static Subject CreateSubject(string name, string curriculumCode, string strandName)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create(strandName, new List<Substrand>(), null);
        var substrand = Substrand.Create("Grammar constructs", new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddSubstrand(substrand);
        substrand.AddContentDescription(contentDescription);

        return subject;
    }

    internal static Subject CreateSubject(string name, string curriculumCode, string strandName, string substrandName)
    {
        var subject = Subject.Create(name, new List<YearLevel>());
        var yearLevel = YearLevel.Create(new List<Strand>(), "Description", "Achievement Standard", YearLevelValue.Foundation, null);
        var strand = Strand.Create(strandName, new List<Substrand>(), null);
        var substrand = Substrand.Create(substrandName, new List<ContentDescription>());
        var contentDescription = ContentDescription.Create("Description", curriculumCode, new List<Elaboration>());

        subject.AddYearLevel(yearLevel);
        yearLevel.AddStrand(strand);
        strand.AddSubstrand(substrand);
        substrand.AddContentDescription(contentDescription);

        return subject;
    }

}
