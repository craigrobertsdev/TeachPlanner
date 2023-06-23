using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifrastructure.Data.Entities;
public class Elaboration
{
    public int Id { get; set; }
    [Required]
    public string Content { get; set; }

    public virtual ContentDescription ContentDescription { get; set; }
}
