using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Application.Common.Extensions;
using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Infrastructure.Curriculum;
internal class GeneralSubjectParser
{
    internal Subject ParseSubject(string[] contentArr, string subjectName, int index)
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
        YearLevelValue yearLevelValue = contentArr[index] == "Foundation"
            ? YearLevelValue.Foundation
            : (YearLevelValue)Enum.Parse(typeof(YearLevelValue), contentArr[index].Replace(" ", ""));

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
        string name = contentArr[index].Substring(8).TrimEnd();
        index += 2;

        // we know there won't be an argument error here
        var strand = Strand.Create(name, substrands: new List<Substrand>()).AsT0;

        while (contentArr[index].StartsWith("Sub-strand"))
        {
            var substrand = GetSubstrand(contentArr, strand, ref index);
            strand.AddSubstrand(substrand);
        }

        return strand;
    }

    private Substrand GetSubstrand(string[] contentArr, Strand strand, ref int index)
    {
        // remove "Sub-strand:" from name
        var name = contentArr[index].Substring(12).TrimEnd();
        var substrand = Substrand.Create(name, new List<ContentDescription>(), strand);

        if (contentArr[index + 1] == "Content descriptions")
        {
            index += 5;
        }
        else
        {
            index++;
        }

        // each time GetContentDescriptions returns, check whether the next line starts with AC9 (instead of another header) and repeat if so.
        while (contentArr[index + 1].StartsWith("AC9"))
        {
            var contentDescription = GetContentDescriptions(contentArr, substrand, ref index);

            substrand.AddContentDescription(contentDescription);
        }

        return substrand;
    }

    private ContentDescription GetContentDescriptions(string[] contentArr, Substrand substrand, ref int index)
    {
        var description = contentArr[index].WithFirstLetterUpper().TrimEnd();
        index++;

        var curriculumCode = contentArr[index].TrimEnd();
        index++;

        var contentDescription = ContentDescription.Create(description, curriculumCode, new List<Elaboration>(), substrand: substrand);

        while (contentArr[index].StartsWith("*"))
        {
            var content = contentArr[index].Substring(2).TrimEnd();
            var elaboration = Elaboration.Create(content, contentDescription);

            contentDescription.AddElaboration(elaboration);
            index++;
        }

        return contentDescription;
    }
}