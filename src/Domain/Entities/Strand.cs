using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Strand {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public virtual ICollection<Substrand> Substrands { get; set; } = new List<Substrand>();

    public YearLevel YearLevel { get; set; }
}
