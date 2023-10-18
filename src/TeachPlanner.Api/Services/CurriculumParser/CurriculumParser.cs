using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Services.CurriculumParser;

public class CurriculumParser : ICurriculumParser
{
    private readonly GeneralSubjectParser _generalSubjectParser;
    private readonly HealthAndPEParser _healthAndPEParser;
    private readonly MathematicsParser _mathematicsParser;
    private readonly List<CurriculumSubject> _subjects;

    public CurriculumParser()
    {
        _subjects = new List<CurriculumSubject>();
        _generalSubjectParser = new GeneralSubjectParser();
        _mathematicsParser = new MathematicsParser();
        _healthAndPEParser = new HealthAndPEParser();
    }

    // Read values from each curriculum document and add appropriate information to the database if not already populated.
    public List<CurriculumSubject> ParseCurriculum()
    {
        var filePaths = Directory.GetFiles(
            //"C:\\Users\\craig\\source\\repos\\TeachPlanner\\src\\TeachPlanner.Curriculum Files");
            "/home/craig/source/TeachPlanner/src/TeachPlanner.Curriculum Files");

        foreach (var file in filePaths)
        {
            var contentArr = LoadFile(file);
            Console.WriteLine(file);

            //string subjectName = file.Split("C:\\Users\\craig\\source\\repos\\TeachPlanner\\src\\TeachPlanner.Curriculum Files")[1];
            var subjectName = file.Split("/home/craig/source/TeachPlanner/src/TeachPlanner.Curriculum Files")[1];

            //subjectName = subjectName.Replace("\\", "").Replace(".docx", "");
            subjectName = subjectName.Replace("/", "").Replace(".docx", "");

            var currElements = "";
            foreach (var content in contentArr)
                if (content == "CURRICULUM ELEMENTS" || content == "Curriculum Elements" ||
                    content == "Curriculum elements")
                {
                    currElements = content;
                    break;
                }

            var index = Array.IndexOf(contentArr, currElements) + 1;

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
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        document.Open(stream, FormatType.Docx);

        // parse entire document into string
        var content = document.GetText();

        // create array of individual lines
        var contentArr = content.Split("\n");

        // remove all carriage returns from created array
        for (var i = 0; i < contentArr.Length; i++) contentArr[i] = contentArr[i].Trim('\r').Trim('\t');

        // remove all empty array entires
        contentArr = contentArr.Where(item => !string.IsNullOrEmpty(item)).ToArray();

        return contentArr;
    }
}