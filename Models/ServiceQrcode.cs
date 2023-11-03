using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class ServiceQrcode
{
    public int QrcodeId { get; set; }

    public int ServicesId { get; set; }

    public int VisitId { get; set; }

    public int? PackageId { get; set; }

    public string Qrcode { get; set; } = null!;

    public virtual StandardService? Package { get; set; }

    public virtual StandardService Services { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;
}
