using Domain.Common.Enums;
using Domain.SubjectAggregates;
using Application.Common.Extensions;

namespace Infrastructure.Curriculum;

internal class MathematicsParser
{
    internal Subject ParseMathematics(string[] contentArr, string subjectName, int index)
    {
        var subject = Subject.Create(subjectName, new List<YearLevel>());

        try
        {
            while (index < contentArr.Length)
            {
                YearLevel yearLevel = ParseYearLevel(contentArr, ref index);
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

    private YearLevel ParseYearLevel(string[] contentArr, ref int index)
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
            var strand = GetStrand(contentArr, ref index);

            yearLevel.AddStrand(strand);

        }

        return yearLevel;
    }

    private Strand GetStrand(string[] contentArr, ref int index)
    {
        // remove "Strand:" from name
        string name = contentArr[index].Substring(8);
        index += 2;

        // safe to unwrap as we know there won't be an argument error here
        var strand = Strand.Create(name, contentDescriptions: new List<ContentDescription>()).AsT0;

        while (contentArr[index].StartsWith("Content descriptions"))
        {
            index += 4;
            var contentDescriptions = GetContentDescriptions(contentArr, strand, ref index);
            strand.AddContentDescriptions(contentDescriptions);
        }

        return strand;
    }

    private List<ContentDescription> GetContentDescriptions(string[] contentArr, Strand strand, ref int index)
    {
        List<ContentDescription> contentDescriptions = new();

        while (contentArr[index + 1].StartsWith("AC9"))
        {
            var description = contentArr[index].WithFirstLetterUpper();
            index++;

            var curriculumCode = contentArr[index];
            index++;

            var contentDescription = ContentDescription.Create(description, curriculumCode, new List<Elaboration>(), strand: strand);

            while (contentArr[index].StartsWith("*"))
            {
                var content = contentArr[index].Substring(2);
                var elaboration = Elaboration.Create(content, contentDescription);

                contentDescription.AddElaboration(elaboration);
                index++;
            }

            contentDescriptions.Add(contentDescription);
        }


        return contentDescriptions;
    }
}
