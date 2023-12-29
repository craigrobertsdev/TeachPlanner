namespace TeachPlanner.Shared.Domain.Users;

public sealed class User {
    public User(string email, string password) {
        Id = new UserId(Guid.NewGuid());
        Email = email;
        Password = password;
    }

    public UserId Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}