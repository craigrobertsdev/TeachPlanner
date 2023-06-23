using Ifrastructure.Data.Entities;

namespace Infrastructure.Data;
public interface ISubjectRepository {
    void DeleteCurriculum();
    List<Subject> GetCurriculum();
    Task SaveCurriculum(List<Subject> curriculum);
}