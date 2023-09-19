using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Domain.Users;
public sealed class User
{
    public Guid Id { get; private set; }
    public Guid TeacherId { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Hash { get; private set; }

    public User(Guid teacherId, string firstName, string lastName, string email, string password, string hash)
    {
        Id = Guid.NewGuid();
        TeacherId = teacherId;
        Email = email;
        Password = password;
        Hash = hash;
    }

    public static (User, Teacher) Create(string firstName, string lastName, string email, string password, string hash)
    {
        var Id = Guid.NewGuid();
        var teacher = Teacher.Create(Id, firstName, lastName);

        return (new User(Id, firstName, lastName, email, password, hash), teacher);
    }
}
