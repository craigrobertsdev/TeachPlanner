namespace TeachPlanner.Shared.Models.Authentication;

public record TokenResponse(string Token, DateTime Expiration);