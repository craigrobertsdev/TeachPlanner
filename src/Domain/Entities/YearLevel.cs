using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class YearLevel {
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string SubjectYearLevel { get; set; }
    public virtual ICollection<Strand> Strands { get; set; } = new List<Strand>();
    [Required]
    public string AchievementStandard { get; set; }
    [Required]
    public string Description { get; set; }
    public virtual Subject Subject { get; set; }
}
