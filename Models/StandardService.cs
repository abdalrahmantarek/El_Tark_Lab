using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class StandardService
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceAvailability { get; set; } = null!;

    public int ServiceAlphaCode { get; set; }

    public string? ServiceUnit { get; set; }

    public bool IsPackage { get; set; }

    public virtual ICollection<NormalRange> NormalRanges { get; set; } = new List<NormalRange>();

    public virtual ServicesCategory ServiceAlphaCodeNavigation { get; set; } = null!;

    public virtual ICollection<ServiceBarcode> ServiceBarcodes { get; set; } = new List<ServiceBarcode>();

    public virtual ICollection<ServicesPricelist> ServicesPricelists { get; set; } = new List<ServicesPricelist>();

    public virtual ICollection<SyndicateService> SyndicateServices { get; set; } = new List<SyndicateService>();

    public virtual ICollection<ServicesPricelist> Packages { get; set; } = new List<ServicesPricelist>();
}
