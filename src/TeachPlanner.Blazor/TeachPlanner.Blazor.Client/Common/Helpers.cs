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
}
