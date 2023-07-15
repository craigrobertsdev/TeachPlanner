using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ContentDescription {
    public int Id { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string CurriculumCode { get; set; }
    public virtual ICollection<Elaboration> Elaborations { get; set; } = new List<Elaboration>();

    public virtual Substrand Substrand { get; set; }
}
