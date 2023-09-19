using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Application.Common.Extensions;
using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Infrastructure.Curriculum;
internal class GeneralSubjectParser
{
    internal Subject ParseSubject(string[] contentArr, string subjectName, int index)
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

        var yearLevel = YearLevel.Create(subject, new List<Strand>(), description, achievementStandard, yearLevelValue, null);

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

        var strand = Strand.Create(yearLevel, name, substrands: new List<Substrand>());

        while (contentArr[index].StartsWith("Sub-strand"))
        {
            var substrand = GetSubstrand(contentArr, ref index, strand);
            strand.AddSubstrand(substrand);
        }

        return strand;
    }

    private Substrand GetSubstrand(string[] contentArr, ref int index, Strand strand)
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
            var contentDescription = GetContentDescriptions(contentArr, ref index, substrand);

            substrand.AddContentDescription(contentDescription);
        }

        return substrand;
    }

    private ContentDescription GetContentDescriptions(string[] contentArr, ref int index, Substrand substrand)
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