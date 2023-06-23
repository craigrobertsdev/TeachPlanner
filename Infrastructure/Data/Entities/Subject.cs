using System.ComponentModel.DataAnnotations;

namespace Ifrastructure.Data.Entities;

public class Subject
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public virtual List<YearLevel> YearLevels { get; set; } = new();
}
