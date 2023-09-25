namespace TeachPlanner.Contracts.Teacher.CreateTeacher;

public record CreateTeacherRequest(Guid TeacherId, string FirstName, string LastName, string Email, string Password);
