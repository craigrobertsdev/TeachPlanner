namespace Ifrastructure.Data.Entities;
public class Substrand
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public virtual List<ContentDescription> ContentDescriptions { get; set; } = new();

    public virtual Strand Strand { get; set; }
}
