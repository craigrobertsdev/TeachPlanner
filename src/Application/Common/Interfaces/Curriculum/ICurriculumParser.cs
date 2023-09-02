using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Common.Interfaces.Curriculum;
public interface ICurriculumParser
{
    List<Subject> ParseCurriculum();
}
