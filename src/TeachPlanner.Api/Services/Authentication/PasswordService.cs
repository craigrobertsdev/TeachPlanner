using BC = BCrypt.Net.BCrypt;

namespace TeachPlanner.Api.Services.Authentication;

public static class PasswordService
{
    public static string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BC.Verify(password, hash);
    }
}