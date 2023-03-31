using System;
using System.Collections.Generic;

namespace AutoRepairShop.Models;

public partial class Repair
{
    public int Id { get; set; }

    public int? CarId { get; set; }


    public int? WorkerId { get; set; }

    public string? RepairType { get; set; }

    public virtual Car? Car { get; set; }

    public virtual Worker? Worker { get; set; }
}