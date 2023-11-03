using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class NormalRange
{
    public int Id { get; set; }

    public int? Standref { get; set; }

    public string AgeRange { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string NormalRange1 { get; set; } = null!;

    public virtual ICollection<RoportResult> RoportResults { get; set; } = new List<RoportResult>();

    public virtual StandardService? StandrefNavigation { get; set; }
}
