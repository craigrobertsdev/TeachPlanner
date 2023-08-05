using Domain.SubjectAggregates;

namespace Application.Common.Interfaces.Curriculum;
public interface ICurriculumParser
{
    List<Subject> ParseCurriculum();
}
