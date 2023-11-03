using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class PatientInvoiceDetail
{
    public int PatientInvoiceDetailId { get; set; }

    public int InvId { get; set; }

    public int VisitId { get; set; }

    public int ServiceId { get; set; }

    public int Serial { get; set; }

    public double ServiceCost { get; set; }

    public string LastUpdateBy { get; set; } = null!;

    public string LastUpdateFrom { get; set; } = null!;

    public DateTime LastUpdateDate { get; set; }

    public virtual PatientInvoice Inv { get; set; } = null!;

    public virtual ServicesPricelist Service { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;
}
