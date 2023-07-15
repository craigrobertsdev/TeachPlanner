using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Subject {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public virtual ICollection<YearLevel> YearLevels { get; set; } = new List<YearLevel>();
}
