namespace TeachPlanner.Api.Domain.Users;

public sealed class User
{
    public UserId Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User(string email, string password)
    {
        Id = new UserId(Guid.NewGuid());
        Email = email;
        Password = password;
    }
}
