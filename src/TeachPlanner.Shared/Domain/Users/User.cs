using System.Security.Claims;

namespace TeachPlanner.Shared.Domain.Users;

public sealed class User {
    public User(string email, string password) {
        Id = new UserId(Guid.NewGuid());
        Email = email;
        Password = password;
    }

    public User() { }

    public UserId Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ClaimsPrincipal ToClaimsPrincipal => new ClaimsPrincipal(
        new ClaimsIdentity(
            new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, Id.Value.ToString()),
                new Claim(ClaimTypes.Name, Email),
            },
            "TeachPlanner"));

    public static User FromClaimsPrincipal(ClaimsPrincipal claimsPrincipal) => new User {
        Id = new UserId(Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value)),
        Email = claimsPrincipal.FindFirst(ClaimTypes.Name).Value
    };
}