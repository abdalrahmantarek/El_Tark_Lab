using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class RoportResult
{
    public int Id { get; set; }

    public int BarcodeId { get; set; }

    public int Result { get; set; }

    public int NormalRangeId { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public virtual ServiceBarcode Barcode { get; set; } = null!;

    public virtual NormalRange NormalRange { get; set; } = null!;
}
