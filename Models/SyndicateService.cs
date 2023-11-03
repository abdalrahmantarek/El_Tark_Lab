using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class SyndicateService
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string ServiceNameAr { get; set; } = null!;

    public double ServicePrice { get; set; }

    public int? StandRef { get; set; }

    public int SyndicateId { get; set; }

    public int ServiceCat { get; set; }

    public virtual ServicesCategory ServiceCatNavigation { get; set; } = null!;

    public virtual StandardService? StandRefNavigation { get; set; }

    public virtual Syndicate Syndicate { get; set; } = null!;
}
