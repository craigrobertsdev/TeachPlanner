namespace TeachPlanner.Api.Contracts.Teachers;

public record TeacherResponse(
    Guid Id,
    string FirstName,
    string LastName);
