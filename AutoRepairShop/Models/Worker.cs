using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Models;

public partial class Worker
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Spec { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Recruitment { get; set; }

    public virtual ICollection<Repair> Repairs { get; } = new List<Repair>();
}
