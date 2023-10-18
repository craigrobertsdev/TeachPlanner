namespace TeachPlanner.Api.Extensions;

public static class StringExtensions
{
    public static string WithFirstLetterUpper(this string str)
    {
        var firstLetter = str[0].ToString().ToUpper();

        return firstLetter + str[1..];
    }
}