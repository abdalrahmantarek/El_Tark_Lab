using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class ServiceBarcode
{
    public int BarcodeId { get; set; }

    public int ServicesId { get; set; }

    public int VisitId { get; set; }

    public int? PackageId { get; set; }

    public string Barcode { get; set; } = null!;

    public int? SampleStatus { get; set; }

    public virtual StandardService? Package { get; set; }

    public virtual ICollection<RoportResult> RoportResults { get; set; } = new List<RoportResult>();

    public virtual ICollection<SampleLog> SampleLogs { get; set; } = new List<SampleLog>();

    public virtual SampleStatus? SampleStatusNavigation { get; set; }

    public virtual ServicesPricelist Services { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;
}
