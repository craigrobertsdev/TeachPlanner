using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Substrand {
    public int Id { get; set; }
    public string? Name { get; set; }
    public virtual ICollection<ContentDescription> ContentDescriptions { get; set; } = new List<ContentDescription>();
    public Strand Strand { get; set; }
}
