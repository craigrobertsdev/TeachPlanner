namespace TeachPlanner.BlazorClient.Authentication.Models;

public class FormResult {
    public bool Succeeded { get; set; }
    public string[] ErrorList { get; set; } = [];
}
