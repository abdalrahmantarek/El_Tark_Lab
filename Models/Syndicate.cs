using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class Syndicate
{
    public int SyndId { get; set; }

    public string SyndName { get; set; } = null!;

    public virtual ICollection<SyndicateService> SyndicateServices { get; set; } = new List<SyndicateService>();
}
