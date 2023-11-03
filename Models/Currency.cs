using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class Currency
{
    public int CId { get; set; }

    public string CurrencyName { get; set; } = null!;

    public virtual ICollection<VendorContract> VendorContracts { get; set; } = new List<VendorContract>();
}
