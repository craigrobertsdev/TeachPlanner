namespace Domain.Models;

public class ContentDescription
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string CurriculumCode { get; set; }
    public List<Elaboration> Elaborations { get; set; } = new();

    public Substrand Substrand { get; set; }
}
