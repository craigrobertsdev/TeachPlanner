using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.UnitTests;

public static class ResourceHelpers {
   public static Resource CreateBasicResource(TeacherId teacherId) {
      return Resource.Create(teacherId, "Resource Name", "Resource URL", false, new SubjectId(Guid.NewGuid()), null);
   } 
}
