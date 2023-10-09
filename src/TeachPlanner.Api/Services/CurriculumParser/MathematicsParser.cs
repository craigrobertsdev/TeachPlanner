using TeachPlanner.Api.Extensions;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Common.Enums;

namespace TeachPlanner.Api.Services.CurriculumParser;

internal class MathematicsParser
{
    internal Subject ParseMathematics(string[] contentArr, string subjectName, int index)
    {
        var subject = Subject.CreateCurriculumSubject(subjectName, new List<YearLevel>());

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

    private YearLevel ParseYearLevel(string[] contentArr, ref int index, Subject subject)
    {
        YearLevelValue yearLevelValue = contentArr[index] == "Foundation" ? YearLevelValue.Foundation : (YearLevelValue)Enum.Parse(typeof(YearLevelValue), contentArr[index].Replace(" ", ""));

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
        } while (!contentArr[index].StartsWith("Strand"));

        var yearLevel = YearLevel.Create(new List<Strand>(), description, achievementStandard, yearLevelValue, null);

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

        var strand = Strand.Create(name, contentDescriptions: new List<ContentDescription>());

        while (contentArr[index].StartsWith("Content descriptions"))
        {
            index += 4;
            var contentDescriptions = GetContentDescriptions(contentArr, ref index, strand);
            strand.AddContentDescriptions(contentDescriptions);
        }

        return strand;
    }

    private List<ContentDescription> GetContentDescriptions(string[] contentArr, ref int index, Strand strand)
    {
        List<ContentDescription> contentDescriptions = new();

        while (contentArr[index + 1].StartsWith("AC9"))
        {
            var description = contentArr[index].WithFirstLetterUpper().TrimEnd();
            index++;

            var curriculumCode = contentArr[index].TrimEnd();
            index++;

            var contentDescription = ContentDescription.Create(description, curriculumCode, new List<Elaboration>());

            while (contentArr[index].StartsWith("*"))
            {
                var content = contentArr[index].Substring(2).TrimEnd().WithFirstLetterUpper();
                var elaboration = Elaboration.Create(content);

                contentDescription.AddElaboration(elaboration);
                index++;
            }

            contentDescriptions.Add(contentDescription);
        }


        return contentDescriptions;
    }
}
