using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class VendorCategory
{
    public int CatId { get; set; }

    public string CatName { get; set; } = null!;

    public string? CatNameAr { get; set; }

    public string AlphaCode { get; set; } = null!;

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
