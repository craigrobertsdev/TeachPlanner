using Domain.Common.Primatives;
using Domain.UserAggregate.ValueObjects;

namespace Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime createdDateTime,
        DateTime updatedDateTime
    )
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static User Create(string firstName, string lastName, string email, string password)
    {
        return new(
            new UserId(Guid.NewGuid()),
            firstName,
            lastName,
            email,
            password,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }

#pragma warning disable CS8618
    private User() { }
}
