using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class Batsh
{
    public int BatchId { get; set; }

    public int VendorId { get; set; }

    public double BatchValue { get; set; }

    public string BatchType { get; set; } = null!;

    public string BatchStatus { get; set; } = null!;

    public int BatchSize { get; set; }

    public DateTime CreationDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public virtual ICollection<BatchDetail> BatchDetails { get; set; } = new List<BatchDetail>();

    public virtual Vendor Vendor { get; set; } = null!;
}
