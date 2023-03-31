using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Models;

public partial class Car
{
    public int Id { get; set; }

    public string? Mark { get; set; }

    public string? RegNumber { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Admission { get; set; }

    public string? Defect { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Ending { get; set; }

    public int? Cost { get; set; }

    public virtual ICollection<Repair> Repairs { get; } = new List<Repair>();
}
