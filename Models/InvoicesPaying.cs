using System;
using System.Collections.Generic;

namespace LAB.Models;

public partial class InvoicesPaying
{
    public int PayingId { get; set; }

    public int InvoiceId { get; set; }

    public double PayingValue { get; set; }

    public DateTime PayingDate { get; set; }

    public string LastUpdateFrom { get; set; } = null!;

    public string LastUpdateBy { get; set; } = null!;

    public virtual PatientInvoice Invoice { get; set; } = null!;
}
