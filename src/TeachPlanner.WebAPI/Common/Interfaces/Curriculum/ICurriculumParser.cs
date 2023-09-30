using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Common.Interfaces.Curriculum;
public interface ICurriculumParser
{
    List<Subject> ParseCurriculum();
}
