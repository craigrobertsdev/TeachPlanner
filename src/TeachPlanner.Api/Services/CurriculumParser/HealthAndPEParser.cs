using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Extensions;

namespace TeachPlanner.Api.Services.CurriculumParser;
internal class HealthAndPEParser
{
    internal CurriculumSubject ParseHealthAndPE(string[] contentArr, string subjectName, int index)
    {
        var subject = CurriculumSubject.Create(subjectName, new List<YearLevel>());

        try
        {
            while (index < contentArr.Length)
            {
                YearLevel yearLevel = ParseYearLevel(contentArr, ref index, subject);
                subject.AddYearLevel(yearLevel);
                // "Australian Curriculum:" appears after all curriculum content for each subject.
                if (contentArr[index].StartsWith("Australian Curriculum:"))
                {
                    break;
                }
            }
        }
        catch { Console.WriteLine("Index: " + index); }
        return subject;
    }

    private YearLevel ParseYearLevel(string[] contentArr, ref int index, CurriculumSubject subject)
    {
        BandLevelValue bandLevelValue;

        if (contentArr[index] == "Foundation")
        {
            bandLevelValue = BandLevelValue.Foundation;
        }
        else
        {
            bandLevelValue = (BandLevelValue)Enum.Parse(typeof(BandLevelValue), contentArr[index].Replace("-", "To").Replace(" ", ""));
        }



        index += 2;
        string description = "";

        do
        {
            if (contentArr[index].StartsWith("*"))
            {
                description += contentArr[index] + "\n";
            }
            else
            {
                description += contentArr[index] + "\n\n";
            }

            index++;
        }
        while (!contentArr[index].StartsWith("Achievement standard"));

        index++;

        // iterate over next x lines to capture the entire achievement standard
        string achievementStandard = "";
        do
        {
            achievementStandard += contentArr[index] + "\n\n";
            index++;
        }
        while (!contentArr[index].StartsWith("Strand"));

        var yearLevel = YearLevel.Create(new List<Strand>(), description, achievementStandard, null, bandLevelValue);

        // continue parsing document until the next line doesn't begin with strand.

        while (contentArr[index].StartsWith("Strand"))
        {
            var strand = GetStrand(contentArr, ref index, yearLevel);

            yearLevel.AddStrand(strand);

        }

        return yearLevel;
    }

    private Strand GetStrand(string[] contentArr, ref int index, YearLevel yearLevel)
    {
        // remove "Strand:" from name
        string name = contentArr[index].Substring(8).TrimEnd();
        index += 2;

        var strand = Strand.Create(name, new List<ContentDescription>());

        while (contentArr[index].StartsWith("Sub-strand"))
        {
            var substrandName = GetSubstrand(contentArr, index);

            if (contentArr[index + 1] == "Content descriptions")
            {
                index += 5;
            }
            else
            {
                index++;
            }

            // each time GetContentDescription returns, check whether the next line starts with AC9 (instead of another header) and repeat if so.
            while (contentArr[index + 1].StartsWith("AC9"))
            {
                var contentDescription = GetContentDescription(contentArr, ref index, substrandName);
                strand.AddContentDescription(contentDescription);
            }
        }

        return strand;
    }

    private static string GetSubstrand(string[] contentArr, int index)
    {
        // remove "Sub-strand:" from name
        return contentArr[index].Substring(12).TrimEnd();
    }

    private ContentDescription GetContentDescription(string[] contentArr, ref int index, string substrandName)
    {
        var description = contentArr[index].WithFirstLetterUpper().TrimEnd();
        index++;

        var curriculumCode = contentArr[index].TrimEnd();
        index++;

        var contentDescription = ContentDescription.Create(description, curriculumCode, new List<Elaboration>(), substrandName);

        while (contentArr[index].StartsWith("*"))
        {
            var content = contentArr[index].Substring(2).TrimEnd().WithFirstLetterUpper();
            var elaboration = Elaboration.Create(content);

            contentDescription.AddElaboration(elaboration);
            index++;
        }

        return contentDescription;
    }
}
