using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.UnitTests.Helpers;
public static class SubjectHelpers {
    public static List<CurriculumSubject> CreateCurriculumSubjects() {
        List<CurriculumSubject> subjects = new();

        for (int i = 0; i < 2; i++) {
            var subject = CurriculumSubject.Create("English" + i, new List<YearLevel>());
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
    public static List<Subject> CreateSubjects() {
        List<Subject> subjects = new();

        for (int i = 0; i < 2; i++) {
            var subject = Subject.Create("English" + i, new List<YearDataContentDescription>());
            subjects.Add(subject);
        }

        return subjects;
    }

    public static CurriculumSubject CreateCurriculumSubjectWithYearLevels() {
        return CurriculumSubject.Create("English", new List<YearLevel>() {
            YearLevel.Create(
                new List<Strand>(),
                "",
                "",
                YearLevelValue.Year1,
                null),
            YearLevel.Create(
                new List<Strand>(),
                "",
                "",
                YearLevelValue.Year6,
                null),
        });
    }
    public static CurriculumSubject CreateCurriculumSubjectWithBandLevels() {
        return CurriculumSubject.Create("English", new List<YearLevel>() {
            YearLevel.Create(
                new List<Strand>(),
                "",
                "",
                null,
                BandLevelValue.Years1To2),
            YearLevel.Create(
                new List<Strand>(),
                "",
                "",
                null,
                BandLevelValue.Years5To6),
        });
    }

}
