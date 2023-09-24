using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Tests.Teachers;
internal static class Helpers
{
    internal static List<Subject> CreateCurriculumSubjects()
    {
        List<Subject> subjects = new();

        for (int i = 0; i < 10; i++)
        {
            var subject = Subject.CreateCurriculumSubject("English" + i, new List<YearLevel>());
            var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description" + i, "Achievement Standard", YearLevelValue.Foundation, null);
            var strand = Strand.Create(yearLevel, "Grammar" + i, new List<Substrand>(), null);
            var substrand = Substrand.Create("Grammar constructs" + i, new List<ContentDescription>(), strand);
            var contentDescription = ContentDescription.Create("Description", "ENG001" + i, new List<Elaboration>(), substrand: substrand);

            subject.AddYearLevel(yearLevel);
            yearLevel.AddStrand(strand);
            strand.AddSubstrand(substrand);
            substrand.AddContentDescription(contentDescription);

            subjects.Add(subject);
        }

        return subjects;
    }
    internal static List<Subject> CreateSubjects()
    {
        List<Subject> subjects = new();

        for (int i = 0; i < 10; i++)
        {
            var subject = Subject.Create("English" + i, new List<YearLevel>());
            var yearLevel = YearLevel.Create(subject, new List<Strand>(), "Description" + i, "Achievement Standard", YearLevelValue.Foundation, null);
            var strand = Strand.Create(yearLevel, "Grammar" + i, new List<Substrand>(), null);
            var substrand = Substrand.Create("Grammar constructs" + i, new List<ContentDescription>(), strand);
            var contentDescription = ContentDescription.Create("Description", "ENG001" + i, new List<Elaboration>(), substrand: substrand);

            subject.AddYearLevel(yearLevel);
            yearLevel.AddStrand(strand);
            strand.AddSubstrand(substrand);
            substrand.AddContentDescription(contentDescription);

            subjects.Add(subject);
        }

        return subjects;
    }
    internal static Teacher CreateTeacher()
    {
        return Teacher.Create(Guid.NewGuid(), "First", "Last");
    }
}
