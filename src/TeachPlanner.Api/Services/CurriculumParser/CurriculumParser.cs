using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Services.CurriculumParser;
public class CurriculumParser : ICurriculumParser
{
    private readonly List<CurriculumSubject> _subjects;
    private readonly GeneralSubjectParser _generalSubjectParser;
    private readonly MathematicsParser _mathematicsParser;
    private readonly HealthAndPEParser _healthAndPEParser;

    public CurriculumParser()
    {
        _subjects = new();
        _generalSubjectParser = new();
        _mathematicsParser = new();
        _healthAndPEParser = new();
    }

    // Read values from each curriculum document and add appropriate information to the database if not already populated.
    public List<CurriculumSubject> ParseCurriculum()
    {
        string[] filePaths = Directory.GetFiles(
            "C:\\Users\\craig\\source\\repos\\TeachPlanner\\src\\TeachPlanner.Curriculum Files");

        foreach (string file in filePaths)
        {

            string[] contentArr = LoadFile(file);
            Console.WriteLine(file);

            string subjectName = file.Split("C:\\Users\\craig\\source\\repos\\TeachPlanner\\src\\TeachPlanner.Curriculum Files")[1];

            subjectName = subjectName.Replace("\\", "").Replace(".docx", "");

            string currElements = "";
            foreach (string content in contentArr)
            {
                if (content == "CURRICULUM ELEMENTS" || content == "Curriculum Elements" || content == "Curriculum elements")
                {
                    currElements = content;
                    break;
                }
            }

            int index = Array.IndexOf(contentArr, currElements) + 1;

            if (subjectName == "Mathematics")
            {
                var subject = _mathematicsParser.ParseMathematics(contentArr, subjectName, index);
                _subjects.Add(subject);
            }
            else if (subjectName == "Health and Physical Education")
            {
                var subject = _healthAndPEParser.ParseHealthAndPE(contentArr, subjectName, index);
                _subjects.Add(subject);
            }
            else
            {
                var subject = _generalSubjectParser.ParseSubject(contentArr, subjectName, index);
                _subjects.Add(subject);
            }
        }

        return _subjects;
    }

    private static string[] LoadFile(string filePath)
    {
        WordDocument document = new();
        using FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        document.Open(stream, FormatType.Docx);

        // parse entire document into string
        string content = document.GetText();

        // create array of individual lines
        string[] contentArr = content.Split("\n");

        // remove all carriage returns from created array
        for (int i = 0; i < contentArr.Length; i++)
        {
            contentArr[i] = contentArr[i].Trim('\r').Trim('\t');
        }

        // remove all empty array entires
        contentArr = contentArr.Where(item => !string.IsNullOrEmpty(item)).ToArray();

        return contentArr;
    }
}
