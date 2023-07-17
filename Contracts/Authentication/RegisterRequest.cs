namespace Contracts.Authentication;

public record RegisterRequest(string FirstName, string lastName, string Email, string Password);