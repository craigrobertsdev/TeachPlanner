using Domain.Entities;

namespace Domain.Abstractions;

public interface ISubjectRepository {
    void DeleteCurriculum();
    List<Subject> GetCurriculum();
    Task SaveCurriculum(List<Subject> curriculum);
}