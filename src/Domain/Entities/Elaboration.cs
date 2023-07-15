using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Elaboration {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Content { get; set; }
    public virtual ContentDescription ContentDescription { get; set; }
}
