﻿using Application.Common.Extensions;
using Domain.Common.Enums;
using Domain.SubjectAggregates;

namespace Infrastructure.Curriculum;
internal class HealthAndPEParser
{
    internal Subject ParseHealthAndPE(string[] contentArr, string subjectName, int index)
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
        BandLevelValue bandLevelValue;

        if (contentArr[index] == "Foundation")
        {
            bandLevelValue = BandLevelValue.Foundation;
        }
        else
        {
            /*            string[] band = contentArr[index].Trim().Split('-');
                        band = band.Select(x => x.Trim()).ToArray();

                        string bandValue = string.Join("To", band);
            */
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
        var name = contentArr[index].Substring(12);
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
            var contentDescription = GetContentDescriptions(contentArr, ref index);

            substrand.AddContentDescription(contentDescription);
        }

        return substrand;
    }

    private ContentDescription GetContentDescriptions(string[] contentArr, ref int index)
    {
        var description = contentArr[index].WithFirstLetterUpper();
        index++;

        var curriculumCode = contentArr[index];
        index++;

        var contentDescription = ContentDescription.Create(description, curriculumCode, new List<Elaboration>());

        while (contentArr[index].StartsWith("*"))
        {
            var content = contentArr[index].Substring(2);
            var elaboration = Elaboration.Create(content, contentDescription);

            contentDescription.AddElaboration(elaboration);
            index++;
        }

        return contentDescription;
    }
}
