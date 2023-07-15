//using Domain.Abstractions;
//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//
//namespace Infrastructure.Persistence;
//
//public class SubjectRepository : Repository<Subject> {
//    private readonly ApplicationDbContext _dbContext;
//
//    public SubjectRepository(ApplicationDbContext dbContext) : base(dbContext) {
//        _dbContext = dbContext;
//    }
//
//    // Loads the entire curriculum including all subset data
//    public List<Subject> GetCurriculum() {
//
//        var subjects = _dbContext.Subjects
//            .Include(subject => subject.YearLevels)
//            .ThenInclude(yearLevel => yearLevel.Strands)
//            .ThenInclude(strand => strand.Substrands)
//            .ThenInclude(substrand => substrand.ContentDescriptions)
//            .ThenInclude(contentDescription => contentDescription.Elaborations)
//            .ToList();
//
//        return subjects;
//    }
//
//    public async Task SaveCurriculum(List<Subject> curriculum) {
//        foreach (var subject in curriculum) {
//            await _dbContext.Subjects.AddAsync(subject);
//        }
//        _dbContext.SaveChanges();
//    }
//
//    public void DeleteCurriculum() {
//        foreach (var subject in _dbContext.Subjects) {
//            _dbContext.Subjects.Remove(subject);
//        }
//
//        _dbContext.SaveChanges();
//    }
//
//    List<Subject> ISubjectRepository.GetCurriculum() {
//        throw new NotImplementedException();
//    }
//
//}
