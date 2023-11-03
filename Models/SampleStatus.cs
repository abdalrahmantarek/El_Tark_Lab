using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class SampleStatus
{
    public int StatusId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<SampleLog> SampleLogs { get; set; } = new List<SampleLog>();

    public virtual ICollection<ServiceBarcode> ServiceBarcodes { get; set; } = new List<ServiceBarcode>();
}
