namespace TeachPlanner.Application.Common.Extensions;

public static class StringExtensions
{
    public static string WithFirstLetterUpper(this string str)
    {
        string firstLetter = str[0].ToString().ToUpper();

        return firstLetter + str[1..];
    }
}
