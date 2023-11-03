using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class SampleLog
{
    public int LogId { get; set; }

    public int BarcodeId { get; set; }

    public int StatusId { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public virtual ServiceBarcode Barcode { get; set; } = null!;

    public virtual SampleStatus Status { get; set; } = null!;
}
