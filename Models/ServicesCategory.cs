using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class ServicesCategory
{
    public int ServiceCatId { get; set; }

    public string ServiceCatName { get; set; } = null!;

    public string ServiceCatNameAr { get; set; } = null!;

    public string AlphaCode { get; set; } = null!;

    public virtual ICollection<ServicesPricelist> ServicesPricelists { get; set; } = new List<ServicesPricelist>();

    public virtual ICollection<StandardService> StandardServices { get; set; } = new List<StandardService>();

    public virtual ICollection<SyndicateService> SyndicateServices { get; set; } = new List<SyndicateService>();
}
