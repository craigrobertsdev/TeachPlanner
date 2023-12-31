namespace TeachPlanner.Blazor.Client.Common;

public static class Helpers {
    public static string CreateCssClass(string className) {
        var words = className.Split(" ");
        string result = "";
        
        for (int i = 0; i < words.Length; i++) {
            if (i == 0) {
                result += words[i].ToLower();
            } else {
                result += FirstLetterToUpper(words[i].ToLower());
            }
        }

        return result;
    } 

    private static string FirstLetterToUpper(string str) => char.ToUpper(str[0]) + str[1..]; 

    public static TimeOnly GetTimeFromString(string time) { // hh:mm:ss
        int hours = int.Parse(time.Substring(0, 2));
        int minutes = int.Parse(time.Substring(3, 2));
        int seconds = int.Parse(time.Substring(6, 2));

        return new TimeOnly(hours, minutes, seconds);
    }

    public static TimeOnly GetTimeFromDate(DateTime date) {
        return new TimeOnly(date.Hour, date.Minute, date.Second);
    }

    /// <summary>
    /// Returns a string in the format of "hh:mm[am/pm]"
    /// </summary>
    public static string GetTimeForCalendar(TimeOnly time) {
        string ampm = time.Hour >= 12 ? "pm" : "am";
        return $"{time.Hour}:{time.Minute:00}{ampm}";
    }
}
