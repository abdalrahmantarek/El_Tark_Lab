using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class VendorContractType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public string? TypeNameAr { get; set; }

    public int? Parent { get; set; }

    public virtual ICollection<VendorContract> VendorContracts { get; set; } = new List<VendorContract>();
}
